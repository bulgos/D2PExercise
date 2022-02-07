using System;
using System.Collections.Generic;
using System.IO;
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
        private WeatherFile _weatherFile = new WeatherFile(null);
        private ListCollectionView _weatherCollection;
        
        public WeatherViewModel()
        {
            Initalise();
        }

        public WeatherFile WeatherFile
        {
            get => _weatherFile;
            set => SetProperty(ref _weatherFile, value);
        }

        public ListCollectionView WeatherCollection
        {
            get => _weatherCollection;
            set => SetProperty(ref _weatherCollection, value);
        }

        private void Initalise()
        {
            WeatherCollection = new ListCollectionView(WeatherFile.WeatherDays);
        }
    }
}
