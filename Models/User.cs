using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_re.Model
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password{ get; set; }
       
        public int? UserGroupID { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string Fullname { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }


        [ForeignKey("UserGroupID")]
        public virtual UserGroup UserGroup { get; set; }
    }
}
