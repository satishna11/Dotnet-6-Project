using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_re.Model.ViewModel
{
    public class UserGroupVM
    {
        public int UserGroupID { get; set; }
        public string UserGroupName { get; set; }
        public string UserGroupCode { get; set; }
        public string DisplayName { get; set; }
        public string ShortName { get; set; }
    }
}
