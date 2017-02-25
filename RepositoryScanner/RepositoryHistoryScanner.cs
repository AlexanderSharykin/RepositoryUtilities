using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryScanner.Activity;
using RepositoryScanner.Measures;
using RepositoryScanner.RepositoryConnection;
using RepositoryScanner.Stats;

namespace RepositoryScanner
{
    public class RepositoryHistoryScanner
    {        
        private IRepositoryConnection _service;
        public RepositoryHistoryScanner(IRepositoryConnection service)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            _service = service;
        }

        /// <summary>
        /// Gets or sets ranges of an activity measure for each activity level
        /// </summary>     
        public IDaylyStatsMeasure Measure { get; set; }

        /// <summary>
        /// Gets or sets ranges of an activity distribution
        /// </summary> 
        public IActivityDistribution Distibution { get; set; }

        public PeriodHistory LoadHistory(RepositoryInfo repo)
        {         
            // gets history                       
            var commits = _service.LoadHistory(repo).OrderBy(c=>c.Time).ToList();

            Dictionary<string, List<RevisionInfo>> commitsByAuthor
                = commits.GroupBy(c => c.Author).ToDictionary(gr => gr.Key, gr => gr.OrderBy(c => c.Time).ToList());

            DateTime dateNow, dateStart;
            if (commits.Count > 0)
            {
                dateStart = commits[0].Time;
                dateNow = commits[commits.Count - 1].Time;
            }
            else
                return null;
            dateStart = new DateTime(dateStart.Year, dateStart.Month, dateStart.Day);
            PeriodHistory history = new PeriodHistory(dateStart, dateNow);

            DateTime streakLastDate = dateStart;
            DateTime streakFirstDate = dateStart;
            foreach (var pair in commitsByAuthor)
            {
                int longestStreak = 0;
                PeriodStats periodStats = new PeriodStats();
                periodStats.Author = pair.Key;
                DaylyStats daylyStats = null;                

                // groups commits by day
                foreach (var entry in pair.Value)
                {

                    if (daylyStats == null || daylyStats.Date != entry.Time.Date)
                    {
                        // max commit count
                        if (daylyStats != null)
                        {                            
                            TestMaxCommit(periodStats, daylyStats);
                        }                        
                        daylyStats = new DaylyStats(entry.Time);
                        periodStats[daylyStats.Date] = daylyStats;

                        // longest streak
                        var daysDiff = (daylyStats.Date.Date - streakLastDate.Date).Days;
                        if (daysDiff > 1 &&
                            // doesn't break streak on weekend
                            !(daylyStats.Date.DayOfWeek == DayOfWeek.Monday && daysDiff <= 3))
                        {
                            TestLongestStreak(periodStats, streakFirstDate, streakLastDate, longestStreak);
                            // starts new streak
                            streakFirstDate = daylyStats.Date;
                            streakLastDate = daylyStats.Date;
                            longestStreak = 1;
                        }
                        else
                        {
                            // streak became longer
                            streakLastDate = daylyStats.Date;                            
                            longestStreak++;
                        }
                    }
                    daylyStats.Commits.Add(entry);
                }                
                TestMaxCommit(periodStats, daylyStats);
                TestLongestStreak(periodStats, streakFirstDate, streakLastDate, longestStreak);

                // saves max commit count
                if (history.MaxCommitCount < periodStats.MaxCommitCount)
                {
                    history.MaxCommitCount = periodStats.MaxCommitCount;
                    history.MostCommittingAuthors.Clear();
                }
                if (history.MaxCommitCount <= periodStats.MaxCommitCount)                
                    history.MostCommittingAuthors.Add(periodStats.Author);

                // saves longest streak length
                if (history.LongestStreak < periodStats.LongestStreak)
                {
                    history.LongestStreak = periodStats.LongestStreak;                    
                    history.MostActiveAuthors.Clear();
                }
                if (history.LongestStreak <= periodStats.LongestStreak)
                    history.MostActiveAuthors.Add(periodStats.Author);

                history[periodStats.Author] = periodStats;
            }

            if (Measure != null && Distibution != null)
            // evaluates activity level
            foreach (var author in history.Authors)
            {
                var periodStats = history[author];
                
                foreach (var st in periodStats.Stats)
                {
                    double activity = Measure.Measure(st, history);
                    st.Activity = Distibution.GetLevel(activity);
                }
            }

            return history;
        }

        /// <summary>
        /// Check if current streak is the longest and saves it
        /// </summary>
        /// <param name="periodStats"></param>
        /// <param name="streakFirstDate"></param>
        /// <param name="streakLastDate"></param>
        /// <param name="longestStreak"></param>
        private void TestLongestStreak(PeriodStats periodStats, DateTime streakFirstDate, DateTime streakLastDate, int longestStreak)
        {            
            if (periodStats.LongestStreak <= longestStreak)
            {
                periodStats.LongestStreak = longestStreak;
                periodStats.LongestStreakFirstDate = streakFirstDate;
                periodStats.LongestStreakLastDate = streakLastDate;
            }
        }

        /// <summary>
        /// Check if current commit count is maximal and saves it
        /// </summary>
        private void TestMaxCommit(PeriodStats periodStats, DaylyStats daylyStats)
        {
            if (daylyStats.CommitCount > periodStats.MaxCommitCount)
            {
                periodStats.MaxCommitCount = daylyStats.CommitCount;
                periodStats.MaxCommitDate = daylyStats.Date;                
            }            
        }
    }
}
