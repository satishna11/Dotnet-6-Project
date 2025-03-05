using System;
namespace inventory_re.Models.ViewModel
{
    public class SalesMasterVM
    {
        public int SalesMasterID { get; set; }
        public int customerID { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Narration { get; set; }

        public List<SalesDetailVM> Detail { get; set; }
    }
}

