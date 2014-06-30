using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryScanner.Stats
{
    public class PeriodHistory
    {        
        private readonly Dictionary<string, PeriodStats> _stats;
        public PeriodHistory(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("startDate");
            endDate = endDate.AddDays(1);
            _periodStart = startDate.Midnight();
            _periodEnd = endDate.Midnight();
            int days = (_periodEnd - _periodStart).Days;
            _weeks = days/7 + (days%7 != 0 ? 1 : 0) + (_periodEnd.DayOfWeek < _periodStart.DayOfWeek ? 1 : 0);
            
            _stats = new Dictionary<string, PeriodStats>();
            MostCommittingAuthors = new List<string>();
            MostActiveAuthors = new List<string>();
            MostActiveAuthors = new List<string>();
        }

        private readonly DateTime _periodStart;
        public DateTime PeriodStart
        {
            get { return _periodStart; }
        }

        private readonly DateTime _periodEnd;
        public DateTime PeriodEnd
        {
            get { return _periodEnd; }
        }

        private readonly int _weeks;
        /// <summary>
        /// Gets number of weeks in period
        /// </summary>
        public int Weeks
        {
            get { return _weeks; }
        }

        /// <summary>
        /// Get list of authors33
        /// </summary>
        public IEnumerable<string> Authors
        {
            get { return _stats.Keys.AsEnumerable(); }
        }
        
        /// <summary>
        /// Gets author's stats
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public PeriodStats this[string author]
        {
            get { return _stats[author]; }
            internal set
            {
                if (value == null)
                    return;
                _stats.Add(author, value);
            }
        }

        public IList<string> MostCommittingAuthors { get; private set; }
        
        public IList<string> MostActiveAuthors { get; private set; }

        public int MaxCommitCount { get; internal set; }        

        public int LongestStreak { get; internal set; }
    }
}
