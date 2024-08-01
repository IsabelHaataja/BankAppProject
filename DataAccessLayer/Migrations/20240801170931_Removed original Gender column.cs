using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class RemovedoriginalGendercolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Customers DROP CONSTRAINT CK_Customers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Customers",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(
                @"ALTER TABLE Customers ADD CONSTRAINT CK_Customers 
                CHECK (Gender IN ('Male', 'Female', 'Other'))");

            migrationBuilder.Sql(
                @"UPDATE Customers SET GenderString = 
                CASE 
                    WHEN GenderEnum = 1 THEN 'Male' 
                    WHEN GenderEnum = 2 THEN 'Female' 
                    ELSE 'Other' 
                END");
        }
    }
}
