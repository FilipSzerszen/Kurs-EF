﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kurs_EF.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalItemStateSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkItemStates",
                column: "Value",
                value: "On hold");

            migrationBuilder.InsertData(
                table: "WorkItemStates",
                column: "Value",
                value: "Rejected");
        }

        /// <inheritdoc />
        //dodawanie danych do tabeli (encji) - seed-owanie danych - sposób 2 - podczas migracji
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "Value",
                keyValue: "On hold");

            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "Value",
                keyValue: "Rejected");
        }
    }
}
