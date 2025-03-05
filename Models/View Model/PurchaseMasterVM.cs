using System;
namespace inventory_re.Models.ViewModel
{
    public class PurchaseMasterVM
    {
        public int PurchaseMasterID { get; set; }
        public int VendorID { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Narration { get; set; }

        public List<PurchaseDetailVM> Detail { get; set; }
    }
}

