using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class AspNetUserRoles
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
