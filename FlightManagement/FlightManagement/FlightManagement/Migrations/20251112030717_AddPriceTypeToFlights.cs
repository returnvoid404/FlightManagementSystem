using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceTypeToFlights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Flights",
                newName: "OneWayPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "RoundTripPrice",
                table: "Flights",
                type: "decimal(10,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoundTripPrice",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "OneWayPrice",
                table: "Flights",
                newName: "Price");
        }
    }
}
