using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_re.Model
{
    public class Unit
    {
        [Key]
        public int UnitID { get; set; }
        public string UnitName { get; set; }
        public string UnitCode { get; set; }
        public bool IsActive { get; set; }
    }
}
