using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RebateContracts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConcentrationConversions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RebateContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalGlobalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetGlobalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversionMultiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcentrationConversions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlobalDemands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GlobalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    DemandInMT = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalDemands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GlobalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuantityAdjustments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RebateContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GlobalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    AdjustingQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityAdjustments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RebateContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RebateContracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RebateContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessUnitRebateContract",
                columns: table => new
                {
                    EligibleBusinessUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RebateContractsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUnitRebateContract", x => new { x.EligibleBusinessUnitsId, x.RebateContractsId });
                    table.ForeignKey(
                        name: "FK_BusinessUnitRebateContract_BusinessUnits_EligibleBusinessUnitsId",
                        column: x => x.EligibleBusinessUnitsId,
                        principalTable: "BusinessUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessUnitRebateContract_RebateContracts_RebateContractsId",
                        column: x => x.RebateContractsId,
                        principalTable: "RebateContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RebateRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RebateContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PerMT = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RebateRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RebateRules_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RebateRules_RebateContracts_RebateContractId",
                        column: x => x.RebateContractId,
                        principalTable: "RebateContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TieredRebates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RebateRuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EndVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TieredRebates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TieredRebates_RebateRules_RebateRuleId",
                        column: x => x.RebateRuleId,
                        principalTable: "RebateRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUnitRebateContract_RebateContractsId",
                table: "BusinessUnitRebateContract",
                column: "RebateContractsId");

            migrationBuilder.CreateIndex(
                name: "IX_RebateRules_ProductId",
                table: "RebateRules",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RebateRules_RebateContractId",
                table: "RebateRules",
                column: "RebateContractId");

            migrationBuilder.CreateIndex(
                name: "IX_TieredRebates_RebateRuleId",
                table: "TieredRebates",
                column: "RebateRuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessUnitRebateContract");

            migrationBuilder.DropTable(
                name: "ConcentrationConversions");

            migrationBuilder.DropTable(
                name: "GlobalDemands");

            migrationBuilder.DropTable(
                name: "QuantityAdjustments");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "TieredRebates");

            migrationBuilder.DropTable(
                name: "BusinessUnits");

            migrationBuilder.DropTable(
                name: "RebateRules");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RebateContracts");
        }
    }
}
