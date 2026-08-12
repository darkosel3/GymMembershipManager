using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymMembershipManager.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "PasswordHash", "Role", "Username" },
                values: new object[] { 100, new DateTime(2026, 8, 12, 10, 35, 4, 569, DateTimeKind.Local).AddTicks(2886), "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", "Admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 100);
        }
    }
}
