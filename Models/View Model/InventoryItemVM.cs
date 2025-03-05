using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_re.Models.ViewModel
{
    public class InventoryItemsVM
    {

        public int InventoryItemsId { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public int ProductCategoryID { get; set; }
        public int MinAlertQty { get; set; }
        public int SmallestUnitID { get; set; }
        public bool IsActive { get; set; }

        public string ProductCategoryName { get; set; }
        public string SmallestUnitName { get; set; }

    }
}
