using System;
using inventory_re.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_re.Models
{
    public class SalesMaster : BaseDBModel
    {
        [Key]
        public int SalesMasterID { get; set; }
        public int CustomerId { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Narration { get; set; }



        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}

