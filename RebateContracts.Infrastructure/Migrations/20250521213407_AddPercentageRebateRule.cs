using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RebateContracts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPercentageRebateRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PercentageRebateRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RebateContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GlobalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VolumeThreshold = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    PriceThreshold = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    MinShare = table.Column<decimal>(type: "decimal(8,6)", precision: 8, scale: 6, nullable: true),
                    RebatePercent = table.Column<decimal>(type: "decimal(8,6)", precision: 8, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PercentageRebateRules", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PercentageRebateRules");
        }
    }
}
