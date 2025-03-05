using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventory_re.Migrations
{
    public partial class PurchaseDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseDetail",
                columns: table => new
                {
                    PurchaseDetailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitID = table.Column<int>(nullable: false),
                    PurchaseMasterID = table.Column<int>(nullable: false),
                    InventoryItemID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false, defaultValue: 0),
                    Rate = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetail", x => x.PurchaseDetailID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PurchaseDetail");
        }
    }
}
