using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kurs_EF.Migrations
{
    /// <inheritdoc />
    public partial class korekta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItemTags_Tags_TagId",
                table: "WorkItemTags");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItemTags_WorkItems_WorkItemId",
                table: "WorkItemTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkItemTags",
                table: "WorkItemTags");

            migrationBuilder.RenameTable(
                name: "WorkItemTags",
                newName: "WorkItemTag");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItemTags_WorkItemId",
                table: "WorkItemTag",
                newName: "IX_WorkItemTag_WorkItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkItemTag",
                table: "WorkItemTag",
                columns: new[] { "TagId", "WorkItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItemTag_Tags_TagId",
                table: "WorkItemTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItemTag_WorkItems_WorkItemId",
                table: "WorkItemTag",
                column: "WorkItemId",
                principalTable: "WorkItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItemTag_Tags_TagId",
                table: "WorkItemTag");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItemTag_WorkItems_WorkItemId",
                table: "WorkItemTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkItemTag",
                table: "WorkItemTag");

            migrationBuilder.RenameTable(
                name: "WorkItemTag",
                newName: "WorkItemTags");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItemTag_WorkItemId",
                table: "WorkItemTags",
                newName: "IX_WorkItemTags_WorkItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkItemTags",
                table: "WorkItemTags",
                columns: new[] { "TagId", "WorkItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItemTags_Tags_TagId",
                table: "WorkItemTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItemTags_WorkItems_WorkItemId",
                table: "WorkItemTags",
                column: "WorkItemId",
                principalTable: "WorkItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
