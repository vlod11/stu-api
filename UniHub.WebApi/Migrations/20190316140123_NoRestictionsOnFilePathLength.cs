using Microsoft.EntityFrameworkCore.Migrations;

namespace UniHub.WebApi.Migrations
{
    public partial class NoRestictionsOnFilePathLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Files",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Files",
                type: "varchar(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
