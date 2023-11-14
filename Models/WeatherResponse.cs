using Newtonsoft.Json;

// deprecated

namespace EWeather.Models
{
    public class WeatherResponse
    {
        [JsonProperty("actual")]
        public ActualWeather Actual { get; set; }
    }
}
