using Microsoft.EntityFrameworkCore.Migrations;

namespace Tameenk.Identity.Log.DAL.Migrations
{
    public partial class updateDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyCrNumber",
                table: "AuthenticationLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "AuthenticationLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanySponserId",
                table: "AuthenticationLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyVatNumber",
                table: "AuthenticationLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyCrNumber",
                table: "AuthenticationLogs");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "AuthenticationLogs");

            migrationBuilder.DropColumn(
                name: "CompanySponserId",
                table: "AuthenticationLogs");

            migrationBuilder.DropColumn(
                name: "CompanyVatNumber",
                table: "AuthenticationLogs");
        }
    }
}
