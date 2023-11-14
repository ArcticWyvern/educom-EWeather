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

        public string WindDirection { get; set; } = "ZW";

        public string Timestamp { get; set; } = "2023-11-14T09:20:00";

        public DateTime Datestamp { get; set; }


    }
}
