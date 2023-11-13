using Microsoft.EntityFrameworkCore;

namespace EWeatherAPI.Models
{
    public class StationMeasurementContext : DbContext
    {
        public DbSet<StationMeasurement> StationMeasurements { get; set; }

        public StationMeasurementContext(DbContextOptions<StationMeasurementContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}
