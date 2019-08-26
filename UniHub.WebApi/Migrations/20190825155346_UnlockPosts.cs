using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UniHub.WebApi.Migrations
{
    public partial class UnlockPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CurrencyCount",
                table: "Users",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserAvailablePosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(nullable: false),
                    PostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAvailablePosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAvailablePosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAvailablePosts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAvailablePosts_PostId",
                table: "UserAvailablePosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAvailablePosts_UserId",
                table: "UserAvailablePosts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAvailablePosts");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyCount",
                table: "Users",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldDefaultValue: 0m);
        }
    }
}
