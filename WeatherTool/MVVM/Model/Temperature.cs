using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherTool.MVVM.Model
{
    public class Temperature : BindableBase
    {
        private double _reading;
        private TemperatureUnit _unit;

        public Temperature(double tempInDegreesCelsius)
            : this(tempInDegreesCelsius, TemperatureUnit.Celsius) { }

        public Temperature(double temp, TemperatureUnit temperatureType)
        {
            Reading = temp;
            Unit = temperatureType;
        }

        public double Reading
        {
            get => _reading;
            set => SetProperty(ref _reading, value);
        }

        public TemperatureUnit Unit
        {
            get => _unit;
            private set => SetProperty(ref _unit, value);
        }

        public void ConvertTemperature(TemperatureUnit newTemperatureType)
        {

            NormaliseTemperature();

            switch (newTemperatureType)
            {
                case TemperatureUnit.Celsius:
                    break;
                case TemperatureUnit.Fahrenheit:
                    Reading = (Reading * 9 / 5) + 32;
                    break;
                case TemperatureUnit.Kelvin:
                    Reading = Reading + 273.15;
                    break;
            }

            Unit = newTemperatureType;
        }

        private void NormaliseTemperature()
        {
            switch (Unit)
            {
                case TemperatureUnit.Celsius:
                    break;
                case TemperatureUnit.Fahrenheit:
                    Reading = (Reading - 32) * 5 / 9;
                    break;
                case TemperatureUnit.Kelvin:
                    Reading = Reading - 273.15;
                    break;
            }
        }

        public static Temperature CreateTemperature(string temperatureString)
        {
            var regexMatch = Regex.Match(temperatureString, @"(-?[0-9]\d*).(C|F|K)");

            if (!regexMatch.Success)
                throw new ArgumentException("Could not read value as temperature");

            if (!double.TryParse(regexMatch.Groups[1].Value, out double temperatureValue))
                throw new ArgumentException("Could not parse temperature value");

            TemperatureUnit temperatureType;

            switch (regexMatch.Groups[2].Value)
            {
                case "C":
                    temperatureType = TemperatureUnit.Celsius;
                    break;
                case "F":
                    temperatureType = TemperatureUnit.Fahrenheit;
                    break;
                case "K":
                    temperatureType = TemperatureUnit.Kelvin;
                    break;
                default:
                    throw new ArgumentException("Temperature unit is invalid");
            }

            return new Temperature(temperatureValue, temperatureType);
        }

        public static Temperature ComputeAverageTemperature(Temperature temp1, Temperature temp2)
        {
            if (temp1.Unit != temp2.Unit)
                temp2.ConvertTemperature(temp1.Unit);

            double averageTemp = (temp1.Reading + temp2.Reading) / 2;

            return new Temperature(averageTemp, temp1.Unit);
        }

        public override string ToString()
        {
            string tempUnit = "°";

            switch (Unit)
            {
                case TemperatureUnit.Celsius:
                    tempUnit += "C";
                    break;
                case TemperatureUnit.Fahrenheit:
                    tempUnit += "F";
                    break;
                case TemperatureUnit.Kelvin:
                    tempUnit += "K";
                    break;
            }

            return $"{Reading}{tempUnit}";
        }
    }

    public enum TemperatureUnit
    {
        Celsius,
        Fahrenheit,
        Kelvin
    }
}
