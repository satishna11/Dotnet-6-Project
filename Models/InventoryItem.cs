using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using inventory_re.Model;

namespace inventory_re.Models
{
    public class InventoryItems
    {
        [Key]
        public int InventoryItemsId { get; set; }
        public string ItemName { get; set; }

        public string ItemCode { get; set; }
        public int ProductCategoryID { get; set; }
        public int MinAlertQty { get; set; }

        public int SmallestUnitID { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("ProductCategoryID")]
        public virtual ProductCategory ProductCategory { get; set; }

        [ForeignKey("SmallestUnitID")]
        public virtual Unit Unit { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
