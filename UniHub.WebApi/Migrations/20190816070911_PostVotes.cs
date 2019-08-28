using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UniHub.WebApi.Migrations
{
    public partial class PostVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostActions");

            migrationBuilder.DropTable(
                name: "PostActionTypes");

            migrationBuilder.CreateTable(
                name: "PostVoteTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Value = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostVoteTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostVoteTypes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostVotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(nullable: false),
                    VoteTypeId = table.Column<int>(nullable: false),
                    PostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostVotes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostVotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostVotes_PostVoteTypes_VoteTypeId",
                        column: x => x.VoteTypeId,
                        principalTable: "PostVoteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostVotes_PostId",
                table: "PostVotes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostVotes_UserId",
                table: "PostVotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostVotes_VoteTypeId",
                table: "PostVotes",
                column: "VoteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostVoteTypes_UserId",
                table: "PostVoteTypes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostVotes");

            migrationBuilder.DropTable(
                name: "PostVoteTypes");

            migrationBuilder.CreateTable(
                name: "PostActionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PostId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostActionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostActionTypes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostActionTypes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostActions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ActionTypeId = table.Column<int>(nullable: false),
                    PostId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostActions_PostActionTypes_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "PostActionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostActions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostActions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostActions_ActionTypeId",
                table: "PostActions",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostActions_PostId",
                table: "PostActions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostActions_UserId",
                table: "PostActions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostActionTypes_PostId",
                table: "PostActionTypes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostActionTypes_UserId",
                table: "PostActionTypes",
                column: "UserId");
        }
    }
}
