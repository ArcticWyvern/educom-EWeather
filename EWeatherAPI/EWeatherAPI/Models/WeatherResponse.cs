using Newtonsoft.Json;

namespace EWeatherAPI.Models
{
    public class WeatherResponse
    {
        [JsonProperty("actual")]
        public ActualWeather Actual { get; set; }
    }
}
