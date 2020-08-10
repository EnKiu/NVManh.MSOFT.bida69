using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class Unit: Entity
    {
        public Unit()
        {
            Inventory = new HashSet<Inventory>();
        }

        public Guid UnitId { get; set; }
        public string UnitName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
