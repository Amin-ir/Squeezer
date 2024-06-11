using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Squeezer.Migrations
{
    /// <inheritdoc />
    public partial class seedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateRegistered", "Email", "Name", "Password", "UserRole" },
                values: new object[] { -1, new DateTime(2024, 6, 11, 23, 52, 14, 367, DateTimeKind.Local).AddTicks(4089), "admin@squeezer.com", "INITIAL ADMIN", "!#/)zW��C�JJ��", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
