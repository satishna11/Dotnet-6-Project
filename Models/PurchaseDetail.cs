using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using inventory_re.Model;

namespace inventory_re.Models
{
    public class PurchaseDetail
    {
        [Key]
        public int PurchaseDetailID { get; set; }
        public int PurchaseMasterID { get; set; }
        public int InventoryItemID { get; set; }
        public int Quantity { get; set; }
        public int UnitID { get; set; }
        public int Rate { get; set; }


        [ForeignKey("PurchaseMasterID")]
        public virtual PurchaseMaster PurchaseMaster { get; set; }




        [ForeignKey("InventoryItemID")]
        public virtual InventoryItems InventoryItems { get; set; }



        [ForeignKey("UnitID")]
        public virtual Unit Unit { get; set; }
    }
}
