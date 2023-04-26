using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kurs_EF.Migrations
{
    /// <inheritdoc />
    public partial class dodanoDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkitemTag_Tags_TagId",
                table: "WorkitemTag");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkitemTag_WorkItems_WorkItemId",
                table: "WorkitemTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkitemTag",
                table: "WorkitemTag");

            migrationBuilder.RenameTable(
                name: "WorkitemTag",
                newName: "WorkItemTags");

            migrationBuilder.RenameIndex(
                name: "IX_WorkitemTag_WorkItemId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "WorkitemTag");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItemTags_WorkItemId",
                table: "WorkitemTag",
                newName: "IX_WorkitemTag_WorkItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkitemTag",
                table: "WorkitemTag",
                columns: new[] { "TagId", "WorkItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkitemTag_Tags_TagId",
                table: "WorkitemTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkitemTag_WorkItems_WorkItemId",
                table: "WorkitemTag",
                column: "WorkItemId",
                principalTable: "WorkItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
