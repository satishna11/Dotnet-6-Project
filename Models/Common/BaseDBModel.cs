using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using inventory_re.Model;

namespace inventory_re.Models.Common
{
    public class BaseDBModel
    {
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }



        [ForeignKey("CreatedBy")]
        public virtual Users CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public virtual Users ModifiedByUser { get; set; }
    }
}
