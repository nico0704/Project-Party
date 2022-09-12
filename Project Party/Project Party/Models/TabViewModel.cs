using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Project_Party.Models
{
    public class TabViewModel : ObservableObject
    {
        public string Title { get; private set; }
        public string DayOfWeekGerman { get; private  set; }

        public String BackgroundColor { get; private set; }
        public ObservableCollection<Party> Partys { get; set; }
        public bool _isSelected;
        public TabViewModel(ObservableCollection<Party> partys, DateTime dateTime)
        {
            var culture = new System.Globalization.CultureInfo("de-DE");
            DayOfWeekGerman = culture.DateTimeFormat.GetDayName(dateTime.DayOfWeek);

            this.Title = dateTime.Day +"." + dateTime.Month;
            this.Partys = partys;
            IsSelected = false;

            if (dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Friday || dateTime.DayOfWeek == DayOfWeek.Saturday)
                BackgroundColor = "Pink";
            else BackgroundColor = "Transparent";
        }


        public bool IsSelected { get => _isSelected; set => SetProperty(ref _isSelected, value); }

    }
}
