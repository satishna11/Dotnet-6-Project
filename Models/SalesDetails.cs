using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using inventory_re.Model;

namespace inventory_re.Models
{
    public class SalesDetail
    {
        [Key]
        public int SalesDetailID { get; set; }
        public int SalesMasterID { get; set; }
        public int InventoryItemID { get; set; }
        public int Quantity { get; set; }
        public int UnitID { get; set; }
        public int Rate { get; set; }


        [ForeignKey("SalesMasterID")]
        public virtual SalesMaster SalesMaster { get; set; }




        [ForeignKey("InventoryItemID")]
        public virtual InventoryItems InventoryItems { get; set; }



        [ForeignKey("UnitID")]
        public virtual Unit Unit { get; set; }
    }
}
