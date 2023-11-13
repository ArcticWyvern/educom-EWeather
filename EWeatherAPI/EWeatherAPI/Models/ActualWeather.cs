using Newtonsoft.Json;
using EWeatherAPI.Models;

namespace EWeatherAPI.Models
{
    public class ActualWeather
    {
        [JsonProperty("stationmeasurements")]
        public List<StationMeasurement> StationMeasurements { get; set; }
    }
}
