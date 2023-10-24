using System.ComponentModel.DataAnnotations;


namespace EWeather.Models
{
    public class Weather
    {
        public string Region { get; set; }  
        public decimal Temperature { get; set; }

        public decimal GroundTemperature { get; set; }  

        public decimal PerceivedTemperature { get; set; }

        public decimal RainfalPastHour { get; set; }

        public decimal Sunpower { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

    }
}
