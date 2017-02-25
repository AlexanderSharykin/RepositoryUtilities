using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryScanner.Stats;

namespace ViewModels
{
    public class WeekHistoryVm: ObservableModel
    {
        public WeekHistoryVm()
        {
            Stats = new Dictionary<DayOfWeek, DaylyStats>
                    {
                        {DayOfWeek.Monday, null},
                        {DayOfWeek.Tuesday, null},
                        {DayOfWeek.Wednesday, null},
                        {DayOfWeek.Thursday, null},
                        {DayOfWeek.Friday, null},
                        {DayOfWeek.Saturday, null},
                        {DayOfWeek.Sunday, null},
                    };
        }

        public string Title { get; set; }

        public DateTime WeekStart { get; set; }

        public Dictionary<DayOfWeek, DaylyStats> Stats { get; private set; }

        public DaylyStats this[int day]
        {
            get { return Stats[(DayOfWeek)day]; }
            set { Stats[(DayOfWeek)day] = value; }
        }

        public IEnumerable<DaylyStats> StatsList
        {
            get { return Enumerable.Range(0, 7).Select(i => this[i]); }
        }
    }
}
