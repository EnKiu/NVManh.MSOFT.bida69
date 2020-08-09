using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Ref = new HashSet<Ref>();
        }

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid? CustomerGroupId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public string Description { get; set; }
        public string IndentityCard { get; set; }
        public int? LevelId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<Ref> Ref { get; set; }
    }
}
