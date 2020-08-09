using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Ref = new HashSet<Ref>();
        }

        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<Ref> Ref { get; set; }
    }
}
