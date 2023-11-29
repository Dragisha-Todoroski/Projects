using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameLibraryApp.DataAccess.Migrations
{
    public partial class VideoGameApplicationUserNavigationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "VideoGames",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_UserId",
                table: "VideoGames",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoGames_AspNetUsers_UserId",
                table: "VideoGames",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoGames_AspNetUsers_UserId",
                table: "VideoGames");

            migrationBuilder.DropIndex(
                name: "IX_VideoGames_UserId",
                table: "VideoGames");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VideoGames");
        }
    }
}
