using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_re.Model.ViewModel
{
    public class UsersVM
    {
        public int UserID { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; }
        public int UserGroupID { get; set; }
        public string Fullname { get; set; }
        public string? ContactNo { get; set; }
        public string Email { get; set; }


        public string ConfirmPassword { get; set; }
        public bool Agree { get; set; }
    }

    public class LoginVM
    {
        public string? Username { get; set; }
        public string Password { get; set; }
    }
}
