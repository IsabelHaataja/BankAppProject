using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Populatingcustomernumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                WITH OrderedCustomers AS (
                    SELECT 
                        CustomerId,
                        ROW_NUMBER() OVER (ORDER BY CustomerId) AS RowNum
                    FROM Customers
                )
                UPDATE Customers
                SET CustomerNumber = RowNum + 999
                FROM Customers c
                INNER JOIN OrderedCustomers o
                ON c.CustomerId = o.CustomerId;
            ");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerNumber",
                table: "Customers",
                column: "CustomerNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerNumber",
                table: "Customers");
        }
    }
}
