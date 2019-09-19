using Microsoft.EntityFrameworkCore.Migrations;

namespace Tameenk.Identity.Log.DAL.Migrations
{
    public partial class updateDB4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Channel",
                table: "AuthenticationLogs",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Channel",
                table: "AuthenticationLogs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
