using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddTripToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRoundTrip",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReturnFlightId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ReturnFlightId",
                table: "Bookings",
                column: "ReturnFlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_ReturnFlight",
                table: "Bookings",
                column: "ReturnFlightId",
                principalTable: "Flights",
                principalColumn: "FlightID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_ReturnFlight",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ReturnFlightId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IsRoundTrip",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ReturnFlightId",
                table: "Bookings");
        }
    }
}
