using System;
using System.Collections.Generic;
using RepositoryScanner.Activity;

namespace RepositoryScanner.Stats
{
    public class DaylyStats
    {
        private IList<RevisionInfo> _commits;

        public DaylyStats(DateTime date)
        {
            Date = date.Midnight();
            Activity = ActivityLevel.None;
        }        

        public DateTime Date { get; private set; }
        
        public IList<RevisionInfo> Commits
        {
            get
            {
                if (_commits == null)
                    _commits = new List<RevisionInfo>();
                return _commits;
            }
        }

        public int CommitCount
        {
            get
            {
                if (_commits == null)
                    return 0;
                return _commits.Count;
            }
        }

        public ActivityLevel Activity { get; internal set; }
    }
}
