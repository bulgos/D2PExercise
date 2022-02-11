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
        private double _temperatureReading;
        private TemperatureType _temperatureType;

        public Temperature(double tempInDegreesCelsius)
            : this(tempInDegreesCelsius, TemperatureType.Celsius) { }

        public Temperature(double temp, TemperatureType temperatureType)
        {
            TemperatureReading = temp;
            TemperatureType = temperatureType;
        }

        public double TemperatureReading
        {
            get => _temperatureReading;
            set => SetProperty(ref _temperatureReading, value);
        }

        public TemperatureType TemperatureType
        {
            get => _temperatureType;
            private set => SetProperty(ref _temperatureType, value);
        }

        public void ConvertTemperature(TemperatureType newTemperatureType)
        {

            NormaliseTemperature();

            switch (newTemperatureType)
            {
                case TemperatureType.Celsius:
                    break;
                case TemperatureType.Fahrenheit:
                    TemperatureReading = (TemperatureReading * 9 / 5) + 32;
                    break;
                case TemperatureType.Kelvin:
                    TemperatureReading = TemperatureReading + 273.15;
                    break;
            }

            TemperatureType = newTemperatureType;
        }

        private void NormaliseTemperature()
        {
            switch (TemperatureType)
            {
                case TemperatureType.Celsius:
                    break;
                case TemperatureType.Fahrenheit:
                    TemperatureReading = (TemperatureReading - 32) * 5 / 9;
                    break;
                case TemperatureType.Kelvin:
                    TemperatureReading = TemperatureReading - 273.15;
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

            TemperatureType temperatureType;

            switch (regexMatch.Groups[2].Value)
            {
                case "C":
                    temperatureType = TemperatureType.Celsius;
                    break;
                case "F":
                    temperatureType = TemperatureType.Fahrenheit;
                    break;
                case "K":
                    temperatureType = TemperatureType.Kelvin;
                    break;
                default:
                    throw new ArgumentException("Temperature unit is invalid");
            }

            return new Temperature(temperatureValue, temperatureType);
        }

        public static Temperature ComputeAverageTemperature(Temperature temp1, Temperature temp2)
        {
            if (temp1.TemperatureType != temp2.TemperatureType)
                temp2.ConvertTemperature(temp1.TemperatureType);

            double averageTemp = (temp1.TemperatureReading + temp2.TemperatureReading) / 2;

            return new Temperature(averageTemp, temp1.TemperatureType);
        }

        public override string ToString()
        {
            string tempUnit = "°";

            switch (TemperatureType)
            {
                case TemperatureType.Celsius:
                    tempUnit += "C";
                    break;
                case TemperatureType.Fahrenheit:
                    tempUnit += "F";
                    break;
                case TemperatureType.Kelvin:
                    tempUnit += "K";
                    break;
            }

            return $"{TemperatureReading}{tempUnit}";
        }
    }

    public enum TemperatureType
    {
        Celsius,
        Fahrenheit,
        Kelvin
    }
}
