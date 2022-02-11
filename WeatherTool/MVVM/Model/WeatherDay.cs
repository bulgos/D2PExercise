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
        private Temperature _lowTemp;
        private Temperature _highTemp;

        public WeatherDay()
            : this(DateTime.Now, 5, 10, TemperatureUnit.Celsius) { }

        public WeatherDay(DateTime date, double lowTemp, double highTemp, TemperatureUnit temperatureType)
            : this (date, new Temperature(lowTemp, temperatureType), new Temperature(highTemp, temperatureType)) { }

        public WeatherDay(DateTime date, Temperature lowTemp, Temperature highTemp)
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

        public Temperature LowTemp
        {
            get => _lowTemp;
            set
            {
                SetProperty(ref _lowTemp, value);
                OnPropertyChanged(nameof(AverageTemp));
            }
        }

        public Temperature HighTemp 
        { 
            get => _highTemp;
            set
            {
                SetProperty(ref _highTemp, value);
                OnPropertyChanged(nameof(AverageTemp));
            }
        }

        [JsonIgnore]
        public Temperature AverageTemp => Temperature.ComputeAverageTemperature(LowTemp, HighTemp);

        public void ConvertTemperature(TemperatureUnit temperatureType)
        {
            LowTemp.ConvertTemperature(temperatureType);
            HighTemp.ConvertTemperature(temperatureType);
            
            OnPropertyChanged(nameof(LowTemp));
            OnPropertyChanged(nameof(HighTemp));
            OnPropertyChanged(nameof(AverageTemp));
        }
    }
}
