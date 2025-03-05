using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventory_re.Migrations
{
    public partial class AddColumnCustomerIdToSalesMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesMaster_Vendor_VendorID",
                table: "SalesMaster");

            migrationBuilder.RenameColumn(
                name: "VendorID",
                table: "SalesMaster",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesMaster_VendorID",
                table: "SalesMaster",
                newName: "IX_SalesMaster_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesMaster_Customer_CustomerId",
                table: "SalesMaster",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesMaster_Customer_CustomerId",
                table: "SalesMaster");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "SalesMaster",
                newName: "VendorID");

            migrationBuilder.RenameIndex(
                name: "IX_SalesMaster_CustomerId",
                table: "SalesMaster",
                newName: "IX_SalesMaster_VendorID");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesMaster_Vendor_VendorID",
                table: "SalesMaster",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
