using Microsoft.EntityFrameworkCore.Migrations;

namespace UniHub.WebApi.Migrations
{
    public partial class RefactorFieldNames2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "RefreshTokens",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "RefreshTokens",
                newName: "CreatedAtUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "RefreshTokens",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "RefreshTokens",
                newName: "CreatedAt");
        }
    }
}
