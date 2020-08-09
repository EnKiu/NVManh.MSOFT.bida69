using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class AspNetUserLogins
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public Guid UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
