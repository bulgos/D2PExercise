using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherTool.MVVM.Model;

namespace WeatherTool.MVVM.ViewModel
{
    public class WeatherViewModel : BindableBase
    {
        private WeatherDay _weatherDay;

        public WeatherViewModel() { }

        public WeatherDay WeatherDay 
        { 
            get => _weatherDay; 
            set => _weatherDay = value; 
        }
    }
}
