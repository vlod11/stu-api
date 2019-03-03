using Microsoft.EntityFrameworkCore.Migrations;

namespace UniHub.WebApi.Migrations
{
    public partial class AddRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Roles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Roles",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
