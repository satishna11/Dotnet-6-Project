using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_re.Model
{
    public class UserGroup
    {
        [Key]
        public int UserGroupID { get; set; }
        public string UserGroupName { get; set; }
        public string UserGroupCode { get; set; }
        public bool IsActive { get; set; }


        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
