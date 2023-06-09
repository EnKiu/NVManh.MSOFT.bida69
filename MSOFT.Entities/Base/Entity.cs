﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Entities
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyKey : Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyIgnore : Attribute
    {
    }
    public class Entity
    {
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}
