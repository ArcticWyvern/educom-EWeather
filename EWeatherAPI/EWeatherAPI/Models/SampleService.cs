using EWeatherAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EWeatherAPI.Models
{
    public class SampleService
    {
        private readonly ILogger<SampleService> _logger;
        private readonly StationMeasurementContext _dbContext;

        public SampleService(ILogger<SampleService> logger, StationMeasurementContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task DoSomethingAsync()
        {
            await Task.Delay(10);
            _logger.LogInformation("DoSomethingAsync in SampleService was invoked.");
        }

        public async Task UpdateDatabaseAsync(List<StationMeasurement> weatherList)
        {
            foreach ( var measurement in weatherList )
            {
                var existingMeasurement = await _dbContext.StationMeasurements.FindAsync(measurement.Id);

                if (existingMeasurement != null)
                {
                    existingMeasurement.Regio = measurement.Regio;
                    existingMeasurement.Temperature = measurement.Temperature;
                    existingMeasurement.FeelTemperature = measurement.FeelTemperature;
                    existingMeasurement.GroundTemperature = measurement.GroundTemperature;
                    existingMeasurement.RainFallLastHour = measurement.RainFallLastHour;
                    existingMeasurement.Sunpower = measurement.Sunpower;
                    existingMeasurement.WindDirection = measurement.WindDirection;
                    existingMeasurement.Longitude = measurement.Longitude;
                    existingMeasurement.Latitude = measurement.Latitude;

                }
                else
                {
                    _dbContext.StationMeasurements.Add(measurement);
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
