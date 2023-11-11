using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VideoGameLibraryApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialData_XboxNameChange_VideoGamePlatform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VideoGamePlatforms",
                keyColumn: "Id",
                keyValue: "518a4c3e-5a6d-4ca5-b960-ce27a73d4afc",
                column: "Name",
                value: "Xbox 360");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VideoGamePlatforms",
                keyColumn: "Id",
                keyValue: new Guid("282f666e-8760-4945-b7f3-5c6efceb013c"));

            migrationBuilder.DeleteData(
                table: "VideoGamePlatforms",
                keyColumn: "Id",
                keyValue: new Guid("39313d51-d740-417e-8a6a-2cb787e3b7e2"));

            migrationBuilder.DeleteData(
                table: "VideoGamePlatforms",
                keyColumn: "Id",
                keyValue: new Guid("480f079e-8d31-4aa0-9574-a17d892bcdeb"));

            migrationBuilder.DeleteData(
                table: "VideoGamePlatforms",
                keyColumn: "Id",
                keyValue: new Guid("711f2638-ad3a-44b3-8995-f1f4659f8bca"));

            migrationBuilder.DeleteData(
                table: "VideoGamePlatforms",
                keyColumn: "Id",
                keyValue: new Guid("7a865638-9fb7-44a0-86cb-561260753241"));

            migrationBuilder.DeleteData(
                table: "VideoGamePlatforms",
                keyColumn: "Id",
                keyValue: new Guid("7e0cf15e-878c-4cfe-8e4b-afdbc6e4e27c"));

            migrationBuilder.DeleteData(
                table: "VideoGamePlatforms",
                keyColumn: "Id",
                keyValue: new Guid("bfb89514-e425-4e47-8064-ff1ab378c2b1"));

            migrationBuilder.DeleteData(
                table: "VideoGamePlatforms",
                keyColumn: "Id",
                keyValue: new Guid("f504fadd-eb43-4f9b-ab26-7ece3a4aa436"));

            migrationBuilder.InsertData(
                table: "VideoGamePlatforms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0600e140-c1ad-42d3-a4a9-81f58bac94fd"), "Nintendo Switch" },
                    { new Guid("2b60a4fb-3c38-4fea-9d55-ed03151290e1"), "Xbox Series X/S" },
                    { new Guid("3a031c6c-18ae-42ff-b98d-b5720f86a3a1"), "Android/IOS" },
                    { new Guid("4ff08dae-d223-44fa-9dd0-da11e92dadf1"), "PlayStation 5" },
                    { new Guid("518a4c3e-5a6d-4ca5-b960-ce27a73d4afc"), "Xbox" },
                    { new Guid("a3035bf0-3f33-4eee-a0e6-f96c9f6ddc97"), "Nintendo 64" },
                    { new Guid("ddc9382d-5e3c-453d-a35e-046e48b28f61"), "PC" },
                    { new Guid("f0e834d7-7ddb-4d9f-ab4b-d03615cffd5e"), "PlayStation 4" }
                });
        }
    }
}
