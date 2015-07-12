using System;
using System.Collections.Generic;

namespace Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public int? CustomerId { get; set; }
        public int? ManagerId { get; set; }
        public Manager Manager { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Customer Customer { get; set; }
    }
}