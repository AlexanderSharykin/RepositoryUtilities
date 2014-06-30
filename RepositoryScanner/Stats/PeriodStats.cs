using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryScanner.Stats
{
    /// <summary>
    /// Statistics of author activity in period
    /// </summary>
    public class PeriodStats
    {        
        public PeriodStats()
        {
            _daylyStats = new Dictionary<string, DaylyStats>();         
        }


        public string Author { get; internal set; }

        private Dictionary<string, DaylyStats> _daylyStats;
        public DaylyStats this[DateTime date]
        {
            get
            {
                string day = date.ToShortDateString();
                DaylyStats stats;
                if (_daylyStats.TryGetValue(day, out stats))
                    return stats;
                return null;
            }
            internal set
            {
                string day;
                if (value != null)
                {
                    day = value.Date.ToShortDateString();
                    if (_daylyStats.ContainsKey(day))
                        _daylyStats[day] = value;
                    else
                        _daylyStats.Add(day, value);
                }
                else
                {
                    day = date.ToShortDateString();
                    if (_daylyStats.ContainsKey(day))
                        _daylyStats.Remove(day);                    
                }              
            }
        }

        public IEnumerable<DaylyStats> Stats
        {
            get { return _daylyStats.Values.OrderBy(s => s.Date); }
        }

        public int MaxCommitCount { get; internal set; }

        public DateTime MaxCommitDate { get; internal set; }        

        public int LongestStreak { get; internal set; }

        public DateTime LongestStreakFirstDate { get; internal set; }

        public DateTime LongestStreakLastDate { get; internal set; }
    }
}
