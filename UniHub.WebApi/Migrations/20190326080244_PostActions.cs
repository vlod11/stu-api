using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniHub.WebApi.Migrations
{
    public partial class PostActions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostVotes");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserProfiles",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "PointsCount",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PostActionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true),
                    PostId = table.Column<int>(nullable: true),
                    UsersProfileId = table.Column<int>(nullable: true)
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
                        name: "FK_PostActionTypes_UserProfiles_UsersProfileId",
                        column: x => x.UsersProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostActions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    UsersProfileId = table.Column<int>(nullable: false),
                    ActionTypeId = table.Column<int>(nullable: false),
                    PostId = table.Column<int>(nullable: false)
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
                        name: "FK_PostActions_UserProfiles_UsersProfileId",
                        column: x => x.UsersProfileId,
                        principalTable: "UserProfiles",
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
                name: "IX_PostActions_UsersProfileId",
                table: "PostActions",
                column: "UsersProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PostActionTypes_PostId",
                table: "PostActionTypes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostActionTypes_UsersProfileId",
                table: "PostActionTypes",
                column: "UsersProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostActions");

            migrationBuilder.DropTable(
                name: "PostActionTypes");

            migrationBuilder.DropColumn(
                name: "PointsCount",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserProfiles",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "PostVotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<int>(nullable: false),
                    UsersProfileId = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false)
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
                        name: "FK_PostVotes_UserProfiles_UsersProfileId",
                        column: x => x.UsersProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostVotes_PostId",
                table: "PostVotes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostVotes_UsersProfileId",
                table: "PostVotes",
                column: "UsersProfileId");
        }
    }
}
