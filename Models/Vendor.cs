using System;
using System.ComponentModel.DataAnnotations;

namespace inventory_re.Models
{
	public class Vendor
	{
            [Key]
            public int VendorId { get; set; }
            public string Fullname { get; set; }
            public string Email { get; set; }
            public string Contact { get; set; }

            public string VAT { get; set; }
            public bool IsActive { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? CreatedDate { get; set; }
            public int? ModifiedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }


    }
}




