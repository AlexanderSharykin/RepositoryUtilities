using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using RepositoryScanner.Stats;

namespace ViewModels
{
    public class PeriodHistoryVm: ObservableModel
    {
        private static string[] _months = {
                                              "Jan", "Feb", "Mar",
                                              "Apr", "May", "Jun",
                                              "Jul", "Aug", "Sep",
                                              "Oct", "Nov", "Dec",
                                          };

        private PeriodHistory _history;

        private IList<string> _authors;
        private string _selectedAuthor;        
        private PeriodStats _authorStats;

        private DateTime _minDate;
        private DateTime _maxDate;        

        private ObservableCollection<WeekHistoryVm> _weeks;
        private DaylyStats _selectedDaylyStats;
        
        public PeriodHistory History
        {
            get { return _history; }
            set
            {
                _history = value;
                if (_history != null)
                {
                    Authors = _history.Authors.ToList();
                    MinDate = _history.PeriodStart;
                    MaxDate = _history.PeriodEnd;
                    SelectedAuthor = Authors.FirstOrDefault();
                }
            }
        }

        public DateTime MinDate
        {
            get { return _minDate; }
            set
            {
                _minDate = value;
                OnPropertyChanged();
                SetDateFilter();
            }
        }

        public DateTime MaxDate
        {
            get { return _maxDate; }
            set
            {
                _maxDate = value;
                OnPropertyChanged();
                SetDateFilter();
            }
        }


        public IList<string> Authors
        {
            get { return _authors; }
            set
            {
                _authors = value;
                OnPropertyChanged();
                
            }
        }

        public string SelectedAuthor
        {
            get { return _selectedAuthor; }
            set
            {
                _selectedAuthor = value;
                OnPropertyChanged();
                if (_selectedAuthor != null)
                    UpdateWeeks();
                else
                    Weeks = null;
                SelectedDaylyStats = null;
            }
        }

        public PeriodStats AuthorStats
        {
            get { return _authorStats; }
            set
            {
                _authorStats = value;
                OnPropertyChanged();
            }
        }

        public DaylyStats SelectedDaylyStats
        {
            get { return _selectedDaylyStats; }
            set
            {
                _selectedDaylyStats = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<WeekHistoryVm> Weeks
        {
            get { return _weeks; }
            set
            {
                _weeks = value;
                OnPropertyChanged();
                SetDateFilter();
            }
        }

        private void UpdateWeeks()
        {            
            DateTime date = _history.PeriodStart;
            DateTime weekStart = date;
            var stats = _history[SelectedAuthor];
            AuthorStats = stats;

            int w = 0;            
            var weekVm = new WeekHistoryVm { WeekStart = weekStart };
            var weeks = new ObservableCollection<WeekHistoryVm>{ weekVm };
            while (date <= _history.PeriodEnd)
            {
                var daylyStats = stats[date];

                int day = (int)date.DayOfWeek;
                if (day == 0 && date > _history.PeriodStart)
                {
                    w++;
                    weekVm = new WeekHistoryVm { WeekStart = date };
                    weeks.Add(weekVm);
                }
                weekVm[day] = daylyStats ?? new DaylyStats(date);                

                #region Sets columns header text

                if (day == 0)
                    weekStart = date;

                string headerText;
                if (day == 6 || date == _history.PeriodEnd)
                {
                    int month = weekStart.Month - 1;
                    if (date.Month != weekStart.Month)
                    {
                        if (date.Year != weekStart.Year)
                            headerText = _months[month] + " " + weekStart.Year + "  " + _months[date.Month - 1] +
                                         " " +
                                         date.Year;

                        else headerText = _months[month] + "  " + _months[date.Month - 1];
                    }
                    else headerText = _months[month];
                    weekVm.Title = headerText;
                }

                #endregion

                date = date.AddDays(1);
            }

            SelectedDaylyStats = null;
            Weeks = weeks;
        }

        private void SetDateFilter()
        {
            if (Weeks == null)
                return;
            ICollectionView cv = CollectionViewSource.GetDefaultView(Weeks);
            cv.Filter = o =>
            {
                var w = (WeekHistoryVm) o;
                return w.WeekStart >= MinDate && w.WeekStart <= MaxDate;
            };

        }        
    }
}
