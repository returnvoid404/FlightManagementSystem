using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightManagement.Migrations
{
    /// <inheritdoc />
    public partial class addUserIdToPassenger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Passengers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Passengers");
        }
    }
}
