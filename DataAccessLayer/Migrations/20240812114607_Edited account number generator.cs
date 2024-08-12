using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Editedaccountnumbergenerator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountNumber",
                table: "Accounts");

            migrationBuilder.Sql(@"
                DECLARE @UniqueAccountNumber NVARCHAR(11);
                DECLARE @Id INT;

                DECLARE AccountCursor CURSOR FOR
                SELECT AccountId
                FROM Accounts
                WHERE AccountNumber IS NULL OR AccountNumber = '';

                OPEN AccountCursor;
                FETCH NEXT FROM AccountCursor INTO @Id;

                WHILE @@FETCH_STATUS = 0
                BEGIN
                    -- Generate a unique account number
                    WHILE 1=1
                    BEGIN
                        SET @UniqueAccountNumber = 'ACCT-' + LEFT(CONVERT(VARCHAR(36), NEWID()), 6);

                        -- Check if the generated account number already exists
                        IF NOT EXISTS (SELECT 1 FROM Accounts WHERE AccountNumber = @UniqueAccountNumber)
                        BEGIN
                            BREAK; -- Exit the loop if the generated account number is unique
                        END
                    END

                    -- Update the account with the unique account number
                    UPDATE Accounts
                    SET AccountNumber = @UniqueAccountNumber
                    WHERE AccountId = @Id;

                    FETCH NEXT FROM AccountCursor INTO @Id;
                END

                CLOSE AccountCursor;
                DEALLOCATE AccountCursor;
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
