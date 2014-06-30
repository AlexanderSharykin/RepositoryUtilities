using RepositoryScanner.Stats;

namespace RepositoryScanner.Measures
{
    public class RevisionsCountMeasure: IDaylyStatsMeasure
    {
        /// <summary>
        /// Calculates activity measure of author on certain day, using his best commit count. 
        /// </summary>
        /// <param name="daylyActivity">Dayly activity</param>        
        /// <param name="history">History of commits</param>
        /// <returns></returns>
        public double Measure(DaylyStats daylyActivity, PeriodHistory history)
        {
            if (daylyActivity.CommitCount == 0)
                return 0;
            string author = daylyActivity.Commits[0].Author;
            int maxCommitCount = history[author].MaxCommitCount;
            if (maxCommitCount > 0)
                return (double) daylyActivity.CommitCount/maxCommitCount;
            return 0;
        }
    }
}
