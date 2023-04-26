using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kurs_EF.Migrations
{
    /// <inheritdoc />
    public partial class NewDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Category", "Value" },
                values: new object[,]
                {
                    { 3, null, "Desktop" },
                    { 4, null, "Api" },
                    { 5, null, "Service" }
                });

            migrationBuilder.UpdateData(
                table: "WorkItemStates",
                keyColumn: "Id",
                keyValue: 2,
                column: "Value",
                value: "Doing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "WorkItemStates",
                keyColumn: "Id",
                keyValue: 2,
                column: "Value",
                value: "Doiing");
        }
    }
}
