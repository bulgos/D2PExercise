using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTool.MVVM.Model
{
    public class WeatherDay : BindableBase
    {
        private DateOnly _date;
        private int _lowTemp;
        private int _highTemp;

        public WeatherDay(DateOnly date, int lowTemp, int highTemp)
        {
            _date = date;
            _lowTemp = lowTemp;
            _highTemp = highTemp;
        }

        public DateOnly Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public int LowTemp
        {
            get => _lowTemp;
            set => SetProperty(ref _lowTemp, value);
        }

        public int HighTemp 
        { 
            get => _highTemp; 
            set => SetProperty(ref _highTemp, value);
        }
    }
}
