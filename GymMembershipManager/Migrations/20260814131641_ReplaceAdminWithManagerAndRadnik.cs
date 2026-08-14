using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymMembershipManager.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceAdminWithManagerAndRadnik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 999, new DateTime(2026, 8, 14, 15, 16, 41, 646, DateTimeKind.Local).AddTicks(8224), "866485796cfa8d7c0cf7111640205b83076433547577511d81f8030ae99ecea5", "Manager", "manager" },
                    { 1000, new DateTime(2026, 8, 14, 15, 16, 41, 646, DateTimeKind.Local).AddTicks(8269), "73203dfc63612de279e2757774b5616706040fddc6c098b1e0b4561c2b9ab0ba", "Radnik", "radnik" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 999);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1000);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 100, new DateTime(2026, 8, 14, 15, 14, 56, 712, DateTimeKind.Local).AddTicks(8651), "866485796cfa8d7c0cf7111640205b83076433547577511d81f8030ae99ecea5", "Manager", "manager" },
                    { 101, new DateTime(2026, 8, 14, 15, 14, 56, 712, DateTimeKind.Local).AddTicks(8700), "73203dfc63612de279e2757774b5616706040fddc6c098b1e0b4561c2b9ab0ba", "Radnik", "radnik" }
                });
        }
    }
}
