using System;

namespace RepositoryScanner
{
    public static class Extensions
    {
        public static DateTime Midnight (this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }
    }
}
