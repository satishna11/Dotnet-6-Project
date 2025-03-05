
using System;
namespace inventory_re.Models
{
    public class Tax
    {
        public int TaxID { get; set; }
        public string TaxName { get; set; }
        public string TaxCode { get; set; }
        public int Percent { get; set; }
        public bool IsActive { get; set; }

    }
}

