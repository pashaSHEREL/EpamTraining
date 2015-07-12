using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class OrderSummary
    {
        public int OrderId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public int? CustomerId { get; set; }
        public int? ManagerId { get; set; }
        public int? NumberOfItems { get; set; }
        public int? TotalCost { get; set; }
    }
}