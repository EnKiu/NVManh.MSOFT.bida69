using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class ViewRefDetail
    {
        public Guid RefDetailId { get; set; }
        public Guid RefId { get; set; }
        public Guid InventoryId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public Guid? StockId { get; set; }
        public decimal? CostPrice { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string UnitName { get; set; }
        public string InventoryName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
