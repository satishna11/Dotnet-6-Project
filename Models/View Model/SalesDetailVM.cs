using System;
namespace inventory_re.Models.ViewModel
{
    public class SalesDetailVM
    {
        public int SalesDetailID { get; set; }
        public int SalesMasterID { get; set; }
        public int InventoryItemID { get; set; }
        public int Quantity { get; set; }
        public int UnitID { get; set; }
        public int Rate { get; set; }
    }
}

