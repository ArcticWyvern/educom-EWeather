using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EWeatherAPI.Models
{
    public class StationMeasurement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }   
        
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
