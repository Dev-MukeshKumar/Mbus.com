using Microsoft.EntityFrameworkCore.Migrations;

namespace Mbus.com.Migrations
{
    public partial class ticketsColumnUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusName",
                table: "Tickets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Tickets",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusName",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Tickets");
        }
    }
}
