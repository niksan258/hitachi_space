using System;
using System.Collections.Generic;
using System.IO;


namespace HitachiSolutions
{
    public class WeatherCsvParser
    {
        static private void removeFirstColumn(string[] lines)
        {
            for (int i = 0; i < lines.Length; ++i)
            {
                lines[i] = lines[i].Substring(lines[i].IndexOf(',') + 1);
            }
        }

        static public List<WeatherDayModel> Parse(string filePath)
        {
            var weatherDayCollection = new List<WeatherDayModel>();
            string[] lines;
            try
            {
                 lines = File.ReadAllLines(filePath);
            }
            catch(IOException)
            {
                return new List<WeatherDayModel>();
            }

            removeFirstColumn(lines);

            var columns = lines[0].Split(',');

            //in order to create object for every day
            foreach (var column in columns)
            {
                weatherDayCollection.Add(new WeatherDayModel()
                {
                    Day = int.Parse(column)
                });
            }

            for (int i = 1; i < lines.Length; ++i)
            {
                columns = lines[i].Split(',');
                for (int j = 0; j < columns.Length; ++j)
                {
                    switch (i)
                    {
                        case 1:
                            weatherDayCollection[j].Temperature = double.Parse(columns[j]);
                            break;
                        case 2:
                            weatherDayCollection[j].Wind = double.Parse(columns[j]);
                            break;
                        case 3:
                            weatherDayCollection[j].Humidity = double.Parse(columns[j]);
                            break;
                        case 4:
                            weatherDayCollection[j].Precipitation = double.Parse(columns[j]);
                            break;
                        case 5:
                            weatherDayCollection[j].Lightning = ( columns[j] == "Yes" );
                            break;
                        case 6:
                            weatherDayCollection[j].Clouds = (CloudTypeEnum)Enum.Parse(typeof(CloudTypeEnum), columns[j]);
                            break;
                        default: break;
                    }
                }
            }
            return weatherDayCollection;
        }

        static public bool WriteToFile(List<WeatherDayModel> reports, string fileName )
        {

            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(fileName);
            }
            catch (IOException)
            {
                return false;
            }


            writer.WriteLine(Constants.DAY_PARAMETER + ", " + Constants.AVERAGE + ", " + Constants.MAX + ", " + Constants.MIN + ", "
                + Constants.MEDIAN + ", " + Constants.PERFECT_DAY + '-' + reports[reports.Count - 1].Day);

            writer.Write(Constants.TEMPERATURE);
            foreach(var report in reports)
            {
                writer.Write(", " + report.Temperature);
            }
            writer.WriteLine();


            writer.Write(Constants.WIND);
            foreach (var report in reports)
            {
                writer.Write(", " + report.Wind);
            }
            writer.WriteLine();


            writer.Write(Constants.HUMIDITY);
            foreach (var report in reports)
            {
                writer.Write(", " + report.Humidity);
            }
            writer.WriteLine();


            writer.Write(Constants.PRECIPITATION);
            foreach (var report in reports)
            {
                writer.Write(", " + report.Precipitation);
            }
            writer.WriteLine();

            writer.Write(Constants.LIGHTNING);
            for(int i = 0; i < reports.Count; ++i)
            {
                writer.Write(", ");
            }
            writer.Write(reports[reports.Count - 1].Lightning == true ? "Yes" : "No");
            writer.WriteLine();


            writer.Write(Constants.CLOUDS);
            for (int i = 0; i < reports.Count; ++i)
            {
                writer.Write(", ");
            }
            writer.Write(reports[reports.Count - 1].Clouds.ToString());
            writer.WriteLine();

            writer.Close();
            return true;
        }

    }
}
