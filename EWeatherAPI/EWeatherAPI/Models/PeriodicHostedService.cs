using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;
using System.Diagnostics;
using System.Globalization;


namespace EWeatherAPI.Models
{
    public class PeriodicHostedService : BackgroundService
    {
        private readonly ILogger<PeriodicHostedService> _logger;
        private readonly IServiceScopeFactory _factory;

        public PeriodicHostedService(
            ILogger<PeriodicHostedService> logger,
            IServiceScopeFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        private TimeSpan ReadIntervalFromConfigFile()
        {
            TimeSpan defaultInterval = TimeSpan.FromSeconds(5); 

            try
            {
                using StreamReader streamReader = new StreamReader("C:\\Users\\stan2\\source\\repos\\EWeather\\EWeatherAPI\\EWeatherAPI\\Config.txt");
                string? line = streamReader.ReadLine();

                if(line != null)
                {
                    int start = line.IndexOf(':');

                    int interval = Convert.ToInt32(line.Substring(start + 1));

                    if(interval >= 86400 ) interval = 86400; // 86400 seconds is 1 full day (feels pointless to have interval be more than that)

                    return TimeSpan.FromSeconds(interval);
                }
                _logger.LogError($"Failed to read interval from Config.txt it appears to be empty.");
                return defaultInterval;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to read interval from Config.txt with message: \n{ex.Message}.");
                return defaultInterval;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TimeSpan interval = ReadIntervalFromConfigFile();
            while (!stoppingToken.IsCancellationRequested)
            {

                try
                {
                    await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                    SampleService sampleService = asyncScope.ServiceProvider.GetRequiredService<SampleService>();
                    await sampleService.DoSomethingAsync();
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
                    if (weatherResponse?.Actual?.StationMeasurements != null)
                    {
                        List<StationMeasurement> weatherList = weatherResponse.Actual.StationMeasurements;

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

                        await sampleService.UpdateDatabaseAsync(weatherList);
                    }
                    

                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        $"Failed to execute PeriodicHostedService with exception message \n{ex.Message}.");
                }

                await Task.Delay(interval, stoppingToken);
            }
        }
    }
}
