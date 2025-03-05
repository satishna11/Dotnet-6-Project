using System;
using System.ComponentModel.DataAnnotations;
using inventory_re.Models.Common;

namespace inventory_re.Models
{
	
	
        public class Customer : BaseDBModel
        {
            [Key]
            public int CustomerId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Contact { get; set; }
            public double VAT { get; set; }

        }
    }


