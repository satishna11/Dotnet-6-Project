using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventory_re.Migrations
{
    public partial class InventoryItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    InventoryItemsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategoryID = table.Column<int>(type: "int", nullable: false),
                    MinAlertQty = table.Column<int>(type: "int", nullable: false),
                    SmallestUnitID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.InventoryItemsId);
                    table.ForeignKey(
                        name: "FK_InventoryItems_ProductCategory_ProductCategoryID",
                        column: x => x.ProductCategoryID,
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Unit_SmallestUnitID",
                        column: x => x.SmallestUnitID,
                        principalTable: "Unit",
                        principalColumn: "UnitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ProductCategoryID",
                table: "InventoryItems",
                column: "ProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_SmallestUnitID",
                table: "InventoryItems",
                column: "SmallestUnitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryItems");
        }
    }
}
