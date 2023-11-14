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


        //duplicate removal code for development only:
        public void RemoveDuplicates()
        {
            var duplicateGroups = _dbContext.StationMeasurements
                .AsEnumerable()
                .GroupBy(m => new { m.Regio, Hour = m.Datestamp.Hour })
                .Where(g => g.Count() > 1);

            var duplicates = duplicateGroups
                .SelectMany(g => g.Skip(1))
                .ToList();

            foreach (var duplicate in duplicates)
            {
                _dbContext.Entry(duplicate).State = EntityState.Detached;
                _dbContext.StationMeasurements.Remove(duplicate);
            }


            _logger.LogInformation("\n\nSampleService deleted duplicates\n\n");
        }


        // Updates database when new data is pulled from buienradar api and checks if there is already an entry in the same hour,
        // if there is one it gets replaced otherwsie it adds a new entry to the db.
        // This ensures one entry per hour and the most up to date entry to be in the db
        // The choice of hour is chosen simply since a day is too large an interval and would lead to only keeping the latest value on a day
        // which most of the time is going to be at night (when this would be used in real applications) 
        // and less than an hour would fill the db too quickly
        public async Task UpdateDatabaseAsync(List<StationMeasurement> weatherList) 
        {

            // RemoveDuplicates();

            foreach ( var measurement in weatherList )
            {
                var existingMeasurement = await _dbContext.StationMeasurements
                    .FirstOrDefaultAsync(existing => existing.Regio == measurement.Regio && existing.Datestamp.Hour == measurement.Datestamp.Hour);

                if (existingMeasurement != null)
                {
                    //update existing entry with updated values
                    existingMeasurement.Regio = measurement.Regio;
                    existingMeasurement.Temperature = measurement.Temperature;
                    existingMeasurement.FeelTemperature = measurement.FeelTemperature;
                    existingMeasurement.GroundTemperature = measurement.GroundTemperature;
                    existingMeasurement.RainFallLastHour = measurement.RainFallLastHour;
                    existingMeasurement.Sunpower = measurement.Sunpower;
                    existingMeasurement.WindDirection = measurement.WindDirection;
                    existingMeasurement.Timestamp = measurement.Timestamp;
                    existingMeasurement.Datestamp = measurement.Datestamp;

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
