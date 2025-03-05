using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_re.Models
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }

        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
