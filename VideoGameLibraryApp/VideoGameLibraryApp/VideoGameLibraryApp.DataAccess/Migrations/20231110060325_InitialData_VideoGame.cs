using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VideoGameLibraryApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialData_VideoGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VideoGames",
                columns: new[] { "Id", "Genre", "IsCoop", "IsMultiplayer", "Publisher", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("74ac5996-3cc6-4bc3-988c-c6f53540624a"), "Survival", false, true, "Mojang", new DateTime(2011, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Minecraft" },
                    { new Guid("a7bee25b-c266-40fb-8dd7-8fe438bae785"), "Shooter", true, true, null, new DateTime(2001, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Halo" },
                    { new Guid("f34443fd-160b-45f9-ae53-cee0913d6587"), "Platformer", true, false, null, new DateTime(2027, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "It Takes Two" }
                });

            migrationBuilder.InsertData(
                table: "VideoGamePlatformAvailability",
                columns: new[] { "Id", "VideoGameId", "VideoGamePlatformId" },
                values: new object[,]
                {
                    { new Guid("485a6e96-72aa-4aed-8142-1b2caf4a60a5"), new Guid("a7bee25b-c266-40fb-8dd7-8fe438bae785"), new Guid("518a4c3e-5a6d-4ca5-b960-ce27a73d4afc") },
                    { new Guid("64eeca2f-0b8e-4221-b07d-f516661345eb"), new Guid("74ac5996-3cc6-4bc3-988c-c6f53540624a"), new Guid("2b60a4fb-3c38-4fea-9d55-ed03151290e1") },
                    { new Guid("95929f89-55c5-4274-9ab5-ee1d93505cbf"), new Guid("74ac5996-3cc6-4bc3-988c-c6f53540624a"), new Guid("518a4c3e-5a6d-4ca5-b960-ce27a73d4afc") },
                    { new Guid("b15f1a71-9d1e-4cee-b59e-e3c1741efd9e"), new Guid("f34443fd-160b-45f9-ae53-cee0913d6587"), new Guid("0600e140-c1ad-42d3-a4a9-81f58bac94fd") },
                    { new Guid("b287e537-f3af-4d50-be91-bbce12c9e8ae"), new Guid("a7bee25b-c266-40fb-8dd7-8fe438bae785"), new Guid("ddc9382d-5e3c-453d-a35e-046e48b28f61") },
                    { new Guid("fc54fdb1-f3ef-4da3-84ee-bce9286fe59e"), new Guid("a7bee25b-c266-40fb-8dd7-8fe438bae785"), new Guid("2b60a4fb-3c38-4fea-9d55-ed03151290e1") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VideoGamePlatformAvailability",
                keyColumn: "Id",
                keyValue: new Guid("485a6e96-72aa-4aed-8142-1b2caf4a60a5"));

            migrationBuilder.DeleteData(
                table: "VideoGamePlatformAvailability",
                keyColumn: "Id",
                keyValue: new Guid("64eeca2f-0b8e-4221-b07d-f516661345eb"));

            migrationBuilder.DeleteData(
                table: "VideoGamePlatformAvailability",
                keyColumn: "Id",
                keyValue: new Guid("95929f89-55c5-4274-9ab5-ee1d93505cbf"));

            migrationBuilder.DeleteData(
                table: "VideoGamePlatformAvailability",
                keyColumn: "Id",
                keyValue: new Guid("b15f1a71-9d1e-4cee-b59e-e3c1741efd9e"));

            migrationBuilder.DeleteData(
                table: "VideoGamePlatformAvailability",
                keyColumn: "Id",
                keyValue: new Guid("b287e537-f3af-4d50-be91-bbce12c9e8ae"));

            migrationBuilder.DeleteData(
                table: "VideoGamePlatformAvailability",
                keyColumn: "Id",
                keyValue: new Guid("fc54fdb1-f3ef-4da3-84ee-bce9286fe59e"));

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: new Guid("74ac5996-3cc6-4bc3-988c-c6f53540624a"));

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: new Guid("a7bee25b-c266-40fb-8dd7-8fe438bae785"));

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: new Guid("f34443fd-160b-45f9-ae53-cee0913d6587"));
        }
    }
}
