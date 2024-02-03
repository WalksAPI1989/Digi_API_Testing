using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Walks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3ec94a58-1ad5-4df0-ba59-5bc3470720a0"), "Hard" },
                    { new Guid("d14055f3-9faf-4957-af7c-62050c75c3f1"), "Easy" },
                    { new Guid("ffb6f7dc-9f93-46d6-b6dc-5c4e19be1d85"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[] { new Guid("3762c9d5-c95c-4ad0-97fc-478ccd7f0ae4"), "TK", "Siddaganga Hills", "https://www.google.com/imgres?imgurl=https%3A%2F%2Fi0.wp.com%2Fstepstogether.in%2Fwp-content%2Fuploads%2F2017%2F04%2FTpic-12.jpg%3Ffit%3D2520%252C1301%26ssl%3D1&tbnid=HBhd1gFODnJP8M&vet=12ahUKEwjcibm3p8OCAxULUGwGHbfZABgQMygcegUIARCRAQ..i&imgrefurl=https%3A%2F%2Fstepstogether.in%2F2017%2F12%2F17%2Fshivagange%2F&docid=PQ0xc9_VwSETiM&w=2520&h=1301&q=siddagange%20hills&ved=2ahUKEwjcibm3p8OCAxULUGwGHbfZABgQMygcegUIARCRAQ" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("3ec94a58-1ad5-4df0-ba59-5bc3470720a0"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d14055f3-9faf-4957-af7c-62050c75c3f1"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("ffb6f7dc-9f93-46d6-b6dc-5c4e19be1d85"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3762c9d5-c95c-4ad0-97fc-478ccd7f0ae4"));
        }
    }
}
