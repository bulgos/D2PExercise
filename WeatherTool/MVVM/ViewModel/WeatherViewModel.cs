using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WeatherTool.MVVM.Model;
using System.Windows.Forms;

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
            UpdateWeatherFile();
        }

        public RelayCommand OpenWeatherCmd => new RelayCommand(OpenWeatherFile);

        public RelayCommand SaveWeatherCmd => new RelayCommand(SaveWeatherFile);

        public RelayCommand SaveAWeatherCmd => new RelayCommand(SaveAsWeatherFile);

        private void UpdateWeatherFile(WeatherFile weatherFile = null)
        {
            if (weatherFile != null)
                WeatherFile = weatherFile;

            WeatherCollection = new ListCollectionView(WeatherFile.WeatherDays);
        }

        public void OpenWeatherFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var weatherFile = WeatherFile.Deserialize(openFileDialog.FileName);
                    UpdateWeatherFile(weatherFile);
                }
            }
        }

        public void SaveWeatherFile()
        {
            WeatherFile.Serialize();
        }

        public void SaveAsWeatherFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                WeatherFile.Filepath = saveFileDialog.FileName;
                WeatherFile.Serialize();
            }
        }
    }
}
