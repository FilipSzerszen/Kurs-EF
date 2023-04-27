using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kurs_EF.Migrations
{
    /// <inheritdoc />
    public partial class ViewTopAuthorsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW View_TopAuthors AS
            SELECT top 5 u.FullName, Count(*) as [WorkItemsCreated]
            FROM Users u
            JOIN WorkItems wi on wi.AuthorID=u.Id
            GROUP by u.id, u.FullName
            Order by WorkItemsCreated desc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DROP VIEW View_TopAuthors");
        }
    }
}
