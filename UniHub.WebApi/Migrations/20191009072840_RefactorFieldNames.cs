using Microsoft.EntityFrameworkCore.Migrations;

namespace UniHub.WebApi.Migrations
{
    public partial class RefactorFieldNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Users",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "LastVisit",
                table: "Users",
                newName: "LastVisitedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Users",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Universities",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Universities",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Universities",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Teachers",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Teachers",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Teachers",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Subjects",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Subjects",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Subjects",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "RefreshTokens",
                newName: "ExpiredAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Posts",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Posts",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Posts",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Groups",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Groups",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Groups",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Files",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Files",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Faculties",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Faculties",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Faculties",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Comments",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Comments",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Answers",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Answers",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Answers",
                newName: "CreatedAtUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Users",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "LastVisitedAtUtc",
                table: "Users",
                newName: "LastVisit");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Users",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Universities",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Universities",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Universities",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Teachers",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Teachers",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Teachers",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Subjects",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Subjects",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Subjects",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ExpiredAt",
                table: "RefreshTokens",
                newName: "ExpirationDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Posts",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Posts",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Posts",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Groups",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Groups",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Groups",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Files",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Files",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Faculties",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Faculties",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Faculties",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Comments",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Comments",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Answers",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Answers",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Answers",
                newName: "CreatedAt");
        }
    }
}
