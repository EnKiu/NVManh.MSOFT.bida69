using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.Entities
{
    public class History
    {
        public Guid HistoryId { get; set; }
        public string HistoryCode { get; set; }
        public string HistoryName { get; set; }
        public string Content { get; set; }
    }
}
