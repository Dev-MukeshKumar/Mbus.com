using Microsoft.EntityFrameworkCore.Migrations;

namespace Mbus.com.Migrations
{
    public partial class BusColumnChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Buses");

            migrationBuilder.AddColumn<int>(
                name: "TicketPrice",
                table: "Buses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "Buses");

            migrationBuilder.AddColumn<int>(
                name: "TotalPrice",
                table: "Buses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
