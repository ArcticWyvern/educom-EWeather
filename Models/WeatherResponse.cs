using Newtonsoft.Json;

namespace EWeather.Models
{
    public class WeatherResponse
    {
        [JsonProperty("actual")]
        public ActualWeather Actual { get; set; }
    }
}
