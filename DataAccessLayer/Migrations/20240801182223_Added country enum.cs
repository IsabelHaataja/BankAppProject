using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Addedcountryenum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryEnum",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(
                @"UPDATE Customers SET CountryEnum = 
                CASE 
                    WHEN Country = 'Sweden' THEN 1
                    WHEN Country = 'Finland' THEN 2
                    WHEN Country = 'Denmark' THEN 3
                    WHEN Country = 'Norway' THEN 4
                    ELSE 0
                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryEnum",
                table: "Customers");

            migrationBuilder.Sql(
                @"UPDATE Customers SET Country = 
                CASE 
                    WHEN CountryEnum = 1 THEN 'Sweden'
                    WHEN CountryEnum = 2 THEN 'Finland'
                    WHEN CountryEnum = 3 THEN 'Denmark'
                    WHEN CountryEnum = 4 THEN 'Norway'
                    ELSE 'Choose'
                END");
        }
    }
}
