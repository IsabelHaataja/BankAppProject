using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Seedlastprocesseddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "INSERT INTO LastProcesseds (CustomerId, LastProcessedDate) " +
                "SELECT DISTINCT CustomerId, '2024-08-10' " +
                "FROM Dispositions " +
                "WHERE CustomerId NOT IN (SELECT CustomerId FROM LastProcesseds)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "DELETE FROM LastProcesseds WHERE LastProcessedDate = '2024-08-10'");
        }
    }
}
