using Microsoft.EntityFrameworkCore.Migrations;

namespace UniHub.WebApi.Migrations
{
    public partial class PostVotesFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostVoteTypes_Users_UserId",
                table: "PostVoteTypes");

            migrationBuilder.DropIndex(
                name: "IX_PostVoteTypes_UserId",
                table: "PostVoteTypes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PostVoteTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PostVoteTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostVoteTypes_UserId",
                table: "PostVoteTypes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostVoteTypes_Users_UserId",
                table: "PostVoteTypes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
