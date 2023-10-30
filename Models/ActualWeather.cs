using Newtonsoft.Json;

namespace EWeather.Models
{
    public class ActualWeather
    {
        [JsonProperty("stationmeasurements")]
        public List<Weather> StationMeasurements { get; set; }
    }

}
