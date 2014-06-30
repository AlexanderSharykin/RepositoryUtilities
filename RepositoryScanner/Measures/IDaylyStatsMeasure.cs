using RepositoryScanner.Stats;

namespace RepositoryScanner.Measures
{
    /// <summary>
    /// A measure on a set of commits
    /// </summary>
    /// <see cref="http://en.wikipedia.org/wiki/Measure_(mathematics)"/>
    public interface IDaylyStatsMeasure
    {
        /// <summary>
        /// Calculates activity measure of author on certain day, comparing to the whole history of commits. 
        /// </summary>
        /// <param name="daylyActivity">Dayly activity</param>
        /// <param name="history">History of commits</param>
        /// <returns></returns>
        double Measure(DaylyStats daylyActivity, PeriodHistory history);
    }
}