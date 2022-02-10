using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTool.MVVM.Model
{
    public class WeatherFile : BindableBase
    {
        private string _location = "Zurich";
        private string _name = "WeatherInfo";
        private List<WeatherDay> _weatherDays = new List<WeatherDay>();

        private WeatherFile() { }

        public WeatherFile(string filepath)
        {
            if (!string.IsNullOrWhiteSpace(filepath))
            {
                Filepath = filepath;
            }

            Initialise();
        }

        [JsonIgnore]
        private static string DefaultName => "WeatherInfo.json";

        [JsonIgnore]
        private static string DefaultFilepath => Path.Combine(Path.GetTempPath(), DefaultName);

        [JsonIgnore]
        public string Filepath { get; set; } = DefaultFilepath;

        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public List<WeatherDay> WeatherDays
        {
            get => _weatherDays;
            set => SetProperty(ref _weatherDays, value);
        }
        private void Initialise()
        {
            BuildDefault();
            Serialize();
        }

        private void BuildDefault()
        {
            var dateToday = DateTime.Now;

            for (int i = 0; i < 7; i++)
            {
                var date = dateToday.AddDays(-i);
                WeatherDay weatherDay = new WeatherDay(date, 10, 15);
                WeatherDays.Add(weatherDay);
            }
        }

        public void Serialize(string path = null)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);

            if (string.IsNullOrWhiteSpace(path))
            {
                path = Filepath;
            }

            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(json);
            }
        }

        public static WeatherFile Deserialize(string path)
        {
            if (!File.Exists(path))
                return null;

            string json = File.ReadAllText(path);

            var weatherFile = JsonConvert.DeserializeObject<WeatherFile>(json);
            weatherFile.Filepath = path;

            return weatherFile;
        }
    }
}
