using System;
using inventory_re.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_re.Models
{
    public class PurchaseMaster : BaseDBModel
    {
        [Key]
        public int PurchaseMasterID { get; set; }
        public int VendorID { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Narration { get; set; }



        [ForeignKey("VendorID")]
        public virtual Vendor Vendor { get; set; }
    }
}

