using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventory_re.Migrations
{
    public partial class PurchaseMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseMaster",
                columns: table => new
                {
                    PurchaseMasterID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_PurchaseMaster", x => x.PurchaseMasterID);
                    table.ForeignKey(
                        name: "FK_PurchaseMaster_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_PurchaseMaster_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_PurchaseMaster_Vendor_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseMaster_CreatedBy",
                table: "PurchaseMaster",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseMaster_ModifiedBy",
                table: "PurchaseMaster",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseMaster_VendorID",
                table: "PurchaseMaster",
                column: "VendorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseMaster");
        }
    }
}
