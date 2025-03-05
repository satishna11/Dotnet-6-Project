using System;
namespace inventory_re.Models.ViewModel
{
    public class PurchaseDetailVM
    {
        public int PurchaseDetailID { get; set; }
        public int PurchaseMasterID { get; set; }
        public int InventoryItemID { get; set; }
        public int Quantity { get; set; }
        public int UnitID { get; set; }
        public int Rate { get; set; }
    }
}

