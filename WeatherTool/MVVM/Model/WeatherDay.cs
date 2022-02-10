using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTool.MVVM.Model
{
    public class WeatherDay : BindableBase
    {
        private DateTime _date;
        private double _lowTemp;
        private double _highTemp;

        public WeatherDay()
            : this(DateTime.Now, 5, 10) { }

        public WeatherDay(DateTime date, int lowTemp, int highTemp)
        {
            _date = date;
            _lowTemp = lowTemp;
            _highTemp = highTemp;
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public double LowTemp
        {
            get => _lowTemp;
            set
            {
                SetProperty(ref _lowTemp, value);
                OnPropertyChanged(nameof(AverageTemp));
            }
        }

        public double HighTemp 
        { 
            get => _highTemp;
            set
            {
                SetProperty(ref _highTemp, value);
                OnPropertyChanged(nameof(AverageTemp));
            }
        }

        [JsonIgnore]
        public double AverageTemp => (LowTemp + HighTemp)/2;
    }
}
