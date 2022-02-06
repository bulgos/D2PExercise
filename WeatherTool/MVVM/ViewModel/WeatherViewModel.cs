using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WeatherTool.MVVM.Model;

namespace WeatherTool.MVVM.ViewModel
{
    public class WeatherViewModel : BindableBase
    {
        private WeatherDay _weatherDay = new WeatherDay();
        private ListCollectionView _weatherCollection;
        
        

        public WeatherViewModel()
        {
            BuildDefaultCollection();
        }

        public WeatherDay WeatherDay
        {
            get => _weatherDay;
            set => _weatherDay = value;
        }

        public ListCollectionView WeatherCollection
        {
            get => _weatherCollection;
            set => SetProperty(ref _weatherCollection, value);
        }

        private void BuildDefaultCollection()
        {
            List<WeatherDay> weatherList = new List<WeatherDay>();

            var dateToday = DateOnly.FromDateTime(DateTime.Now);

            for (int i = 0; i < 7; i++)
            {
                var date = dateToday.AddDays(-i);
                WeatherDay weatherDay = new WeatherDay(date, 10, 15);
                weatherList.Add(weatherDay);
            }

            WeatherCollection = new ListCollectionView(weatherList);
        }
    }
}
