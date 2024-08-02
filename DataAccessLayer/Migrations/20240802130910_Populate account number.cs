using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Populateaccountnumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                WITH CTE_Duplicates AS (
                    SELECT 
                        AccountId, 
                        ROW_NUMBER() OVER(PARTITION BY AccountNumber ORDER BY (SELECT NULL)) AS RowNum
                    FROM Accounts
                    WHERE AccountNumber IS NULL OR AccountNumber = ''
                       OR AccountNumber IN (
                           SELECT AccountNumber
                           FROM Accounts
                           GROUP BY AccountNumber
                           HAVING COUNT(*) > 1
                       )
                )
                UPDATE Accounts
                SET AccountNumber = 'ACCT-' + LEFT(CONVERT(VARCHAR(36), NEWID()), 6)
                FROM Accounts a
                INNER JOIN CTE_Duplicates d ON a.AccountId = d.AccountId
                WHERE d.RowNum > 1 OR a.AccountNumber IS NULL OR a.AccountNumber = '';
            ");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "Accounts",
                type: "nvarchar(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountNumber",
                table: "Accounts",
                column: "AccountNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountNumber",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "Accounts",
                type: "nvarchar(11)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldNullable: false);
        }
    }
}
