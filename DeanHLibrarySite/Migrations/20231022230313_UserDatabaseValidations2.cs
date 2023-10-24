using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeanHLibrarySite.Migrations
{
    /// <inheritdoc />
    public partial class UserDatabaseValidations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasAdministrator",
                table: "UserTable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAdministrator",
                table: "UserTable");
        }
    }
}
