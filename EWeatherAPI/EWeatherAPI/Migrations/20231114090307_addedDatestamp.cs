using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EWeatherAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedDatestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "StationMeasurements",
                newName: "Timestamp");

            migrationBuilder.AddColumn<DateTime>(
                name: "Datestamp",
                table: "StationMeasurements",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Datestamp",
                table: "StationMeasurements");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "StationMeasurements",
                newName: "TimeStamp");
        }
    }
}
