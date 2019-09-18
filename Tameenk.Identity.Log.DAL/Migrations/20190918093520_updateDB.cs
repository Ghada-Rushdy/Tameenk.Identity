using Microsoft.EntityFrameworkCore.Migrations;

namespace Tameenk.Identity.Log.DAL.Migrations
{
    public partial class updateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AuthenticationLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AuthenticationLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AuthenticationLogs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AuthenticationLogs");
        }
    }
}
