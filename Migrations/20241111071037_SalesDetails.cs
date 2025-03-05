using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventory_re.Migrations
{
    public partial class SalesDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesMaster",
                columns: table => new
                {
                    SalesMasterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorID = table.Column<int>(type: "int", nullable: false),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesMaster", x => x.SalesMasterID);
                    table.ForeignKey(
                        name: "FK_SalesMaster_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_SalesMaster_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_SalesMaster_Vendor_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesDetail",
                columns: table => new
                {
                    SalesDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesMasterID = table.Column<int>(type: "int", nullable: false),
                    InventoryItemID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDetail", x => x.SalesDetailID);
                    table.ForeignKey(
                        name: "FK_SalesDetail_InventoryItems_InventoryItemID",
                        column: x => x.InventoryItemID,
                        principalTable: "InventoryItems",
                        principalColumn: "InventoryItemsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesDetail_SalesMaster_SalesMasterID",
                        column: x => x.SalesMasterID,
                        principalTable: "SalesMaster",
                        principalColumn: "SalesMasterID",
                        onDelete: ReferentialAction.Cascade);
                   
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetail_InventoryItemID",
                table: "SalesDetail",
                column: "InventoryItemID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetail_SalesMasterID",
                table: "SalesDetail",
                column: "SalesMasterID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetail_UnitID",
                table: "SalesDetail",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesMaster_CreatedBy",
                table: "SalesMaster",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SalesMaster_ModifiedBy",
                table: "SalesMaster",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SalesMaster_VendorID",
                table: "SalesMaster",
                column: "VendorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesDetail");

            migrationBuilder.DropTable(
                name: "SalesMaster");
        }
    }
}
