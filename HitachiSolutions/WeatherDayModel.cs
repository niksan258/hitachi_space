namespace HitachiSolutions
{
    public class WeatherDayModel
    {
        public int Day { get; set; }

        public double Temperature { get; set; }

        public double Wind { get; set; }

        public double Humidity { get; set; }

        public double Precipitation { get; set; }

        public bool Lightning { get; set; }

        public CloudTypeEnum Clouds { get; set; }
    }
}
