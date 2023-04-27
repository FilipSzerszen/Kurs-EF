using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kurs_EF.Migrations
{
    /// <inheritdoc />
    public partial class CoordinateToAdressAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Coordinates_Latitude",
                table: "Adresses",
                type: "decimal(18,7)",
                precision: 18,
                scale: 7,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Coordinates_Longitude",
                table: "Adresses",
                type: "decimal(18,7)",
                precision: 18,
                scale: 7,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates_Latitude",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "Coordinates_Longitude",
                table: "Adresses");
        }
    }
}
