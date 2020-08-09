﻿using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class Service
    {
        public Service()
        {
            RefService = new HashSet<RefService>();
        }

        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int? ServiceType { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool InUse { get; set; }
        public DateTime? StartTime { get; set; }

        public virtual ICollection<RefService> RefService { get; set; }
    }
}
