using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EWeatherAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "StationMeasurements");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "StationMeasurements",
                newName: "TimeStamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "StationMeasurements",
                newName: "Longitude");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "StationMeasurements",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
