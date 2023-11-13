using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EWeatherAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StationMeasurements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Regio = table.Column<string>(type: "TEXT", nullable: false),
                    Temperature = table.Column<decimal>(type: "TEXT", nullable: false),
                    GroundTemperature = table.Column<decimal>(type: "TEXT", nullable: false),
                    FeelTemperature = table.Column<decimal>(type: "TEXT", nullable: false),
                    RainFallLastHour = table.Column<decimal>(type: "TEXT", nullable: false),
                    Sunpower = table.Column<decimal>(type: "TEXT", nullable: false),
                    WindDirection = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<decimal>(type: "TEXT", nullable: false),
                    Longitude = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationMeasurements", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StationMeasurements");
        }
    }
}
