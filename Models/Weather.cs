using System.ComponentModel.DataAnnotations;


namespace EWeather.Models
{
    public class Weather
    {
        public string Regio { get; set; }  
        public decimal Temperature { get; set; }

        public decimal GroundTemperature { get; set; }  

        public decimal FeelTemperature { get; set; }

        public decimal RainFallLastHour { get; set; }

        public decimal Sunpower { get; set; }

        public string WindDirection { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

    }
}
