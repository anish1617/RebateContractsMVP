using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RebateContracts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BusinessUnits",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "Default Unit" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessUnits",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
