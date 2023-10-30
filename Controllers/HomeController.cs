using EWeather.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;

namespace EWeather.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            string apiResponse;
            WeatherResponse? weatherResponse;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://data.buienradar.nl/2.0/feed/json"))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(apiResponse);
                }
            }

            // Now you can access the weather data using weatherResponse.Actual.StationMeasurements
            List<Weather> weatherList = weatherResponse?.Actual?.StationMeasurements ?? new List<Weather>();

            // Pass the weatherList to the view
            return View(weatherList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}