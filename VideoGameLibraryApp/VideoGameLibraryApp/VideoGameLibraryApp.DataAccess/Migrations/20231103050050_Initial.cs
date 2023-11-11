using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameLibraryApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoGamePlatforms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGamePlatforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IsMultiplayer = table.Column<bool>(type: "bit", nullable: false),
                    IsCoop = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoGamePlatformAvailability",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoGameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoGamePlatformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGamePlatformAvailability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoGamePlatformAvailability_VideoGamePlatforms_VideoGamePlatformId",
                        column: x => x.VideoGamePlatformId,
                        principalTable: "VideoGamePlatforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoGamePlatformAvailability_VideoGames_VideoGameId",
                        column: x => x.VideoGameId,
                        principalTable: "VideoGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoGamePlatformAvailability_VideoGameId",
                table: "VideoGamePlatformAvailability",
                column: "VideoGameId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGamePlatformAvailability_VideoGamePlatformId",
                table: "VideoGamePlatformAvailability",
                column: "VideoGamePlatformId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoGamePlatformAvailability");

            migrationBuilder.DropTable(
                name: "VideoGamePlatforms");

            migrationBuilder.DropTable(
                name: "VideoGames");
        }
    }
}
