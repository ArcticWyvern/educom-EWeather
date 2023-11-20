﻿using EWeather.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

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
            List<Weather>? weatherList;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7259/api/StationMeasurements/Latest"))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    weatherList = JsonConvert.DeserializeObject<List<Weather>>(apiResponse);
                    if (weatherList != null)
                    {
                        foreach (var measurement in weatherList)
                        {
                            if (measurement.Timestamp != null && measurement.Timestamp != "0.0")
                            {
                                measurement.Datestamp = DateTime.ParseExact(measurement.Timestamp, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                measurement.Datestamp = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                            }
                        }
                    }
                }
            }
            // Pass the weatherList to the view
            return View(weatherList);
        }

        public async Task<IActionResult> Graph(string region, string startDate, string endDate)
        {
            string requestUrl = "https://localhost:7259/api/StationMeasurements/GetByRegionAndDate?region=" + region + "&startDate=" + startDate + "&endDate=" + endDate;
            string apiResponse;
            List<Weather>? weatherList;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(requestUrl))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    weatherList = JsonConvert.DeserializeObject<List<Weather>>(apiResponse);
                    if (weatherList != null)
                    {
                        foreach (var measurement in weatherList)
                        {
                            if (measurement.Timestamp != null && measurement.Timestamp != "0.0")
                            {
                                measurement.Datestamp = DateTime.ParseExact(measurement.Timestamp, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                measurement.Datestamp = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                            }
                        }
                    }
                }
            }

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