using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeanHLibrarySite.Migrations
{
    /// <inheritdoc />
    public partial class AddingIdToReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "BookReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "BookReservations");
        }
    }
}
