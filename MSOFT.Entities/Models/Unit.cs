using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class Unit
    {
        public Unit()
        {
            Inventory = new HashSet<Inventory>();
        }

        public Guid UnitId { get; set; }
        public string UnitName { get; set; }
        public string Description { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
