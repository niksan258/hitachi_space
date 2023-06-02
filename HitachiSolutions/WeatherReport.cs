using System.Collections.Generic;
using System.Linq;

namespace HitachiSolutions
{
    public class WeatherReport
    {
        private List<WeatherDayModel> dayModels;

        public WeatherReport(List<WeatherDayModel> dayModels)
        {
            this.dayModels = dayModels;
        }

        private double GetAverageOfProperty(string propertyName)
        {
            double rowSum = 0;
            foreach (var day in this.dayModels)
            {
                var value = (double)day.GetType()
                    .GetProperty(propertyName)
                    .GetValue(day, null);

                rowSum += value;
            }
            return (rowSum / this.dayModels.Count);
        }

        private double GetMaxOfProperty(string propertyName)
        {
            double maxRowValue = double.MinValue;
            foreach (var day in this.dayModels)
            {
                var value = (double)day.GetType()
                    .GetProperty(propertyName)
                    .GetValue(day, null);
                maxRowValue = value > maxRowValue ? value : maxRowValue;
            }
            return maxRowValue;
        }

        private double GetMinOfProperty(string propertyName)
        {
            double minRowValue = double.MaxValue;
            foreach (var day in this.dayModels)
            {
                var value = (double)day.GetType()
                    .GetProperty(propertyName)
                    .GetValue(day, null);
                minRowValue = value > minRowValue ? minRowValue : value;
            }
            return minRowValue;
        }

        private double GetMedianOfProperty(string propertyName)
        {
            List<double> sortedProperty = new List<double>();
            foreach(var day in this.dayModels)
            {
                var value = (double)day.GetType()
                    .GetProperty(propertyName)
                    .GetValue(day, null);
                sortedProperty.Add(value);
            }
            sortedProperty.Sort();
            if(sortedProperty.Count % 2 == 0)
            {
                return (sortedProperty[sortedProperty.Count / 2] + sortedProperty[sortedProperty.Count / 2 + 1]) / 2;
            }
            else
            {
                return sortedProperty[sortedProperty.Count / 2];
            }
        }

        public WeatherDayModel AverageDayModel()
        {
            WeatherDayModel weatherDayModel = new WeatherDayModel()
            {
                Temperature = GetAverageOfProperty(nameof(weatherDayModel.Temperature)),
                Wind = GetAverageOfProperty(nameof(weatherDayModel.Wind)),
                Humidity = GetAverageOfProperty(nameof(weatherDayModel.Humidity)),
                Precipitation = GetAverageOfProperty(nameof(weatherDayModel.Precipitation))
            };
            return weatherDayModel;
        } 

        public WeatherDayModel MaxDayModel()
        {
            WeatherDayModel weatherDayModel = new WeatherDayModel()
            {
                Temperature = GetMaxOfProperty(nameof(weatherDayModel.Temperature)),
                Wind = GetMaxOfProperty(nameof(weatherDayModel.Wind)),
                Humidity = GetMaxOfProperty(nameof(weatherDayModel.Humidity)),
                Precipitation = GetMaxOfProperty(nameof(weatherDayModel.Precipitation))
            };
            return weatherDayModel;
        }

        public WeatherDayModel MinDayModel()
        {
            WeatherDayModel weatherDayModel = new WeatherDayModel()
            {
                Temperature = GetMinOfProperty(nameof(weatherDayModel.Temperature)),
                Wind = GetMinOfProperty(nameof(weatherDayModel.Wind)),
                Humidity = GetMinOfProperty(nameof(weatherDayModel.Humidity)),
                Precipitation = GetMinOfProperty(nameof(weatherDayModel.Precipitation))
            };
            return weatherDayModel;
        }
        
        public WeatherDayModel MedianDayModel()
        {
            WeatherDayModel weatherDayModel = new WeatherDayModel()
            {
                Temperature = GetMedianOfProperty(nameof(weatherDayModel.Temperature)),
                Wind = GetMedianOfProperty(nameof(weatherDayModel.Wind)),
                Humidity = GetMedianOfProperty(nameof(weatherDayModel.Humidity)),
                Precipitation = GetMedianOfProperty(nameof(weatherDayModel.Precipitation))
            };
            return weatherDayModel;
        }

        public WeatherDayModel PerfectDay()
        {
            List<WeatherDayModel> goodDays = new List<WeatherDayModel>();
            foreach (var day in this.dayModels)
            {
                if ((day.Temperature >= 2 && day.Temperature <= 31)
                    && day.Precipitation == 0
                    && day.Lightning == false
                    && day.Clouds != CloudTypeEnum.Cumulus
                    && day.Clouds != CloudTypeEnum.Nimbus
                    && day.Humidity < 60
                    && day.Wind < 10) goodDays.Add(day);
            }
            
            return goodDays.OrderBy(x => x.Wind)
                .ThenBy(x => x.Humidity)
                .FirstOrDefault();
        }

        public List<WeatherDayModel> ReportTable()
        {
            List<WeatherDayModel> reportTable = new List<WeatherDayModel>();
            reportTable.Add(AverageDayModel());
            reportTable.Add(MaxDayModel());
            reportTable.Add(MinDayModel());
            reportTable.Add(MedianDayModel());
            reportTable.Add(PerfectDay());

            return reportTable;
        }
    }
}
