using System;

namespace Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public int? CustomerId { get; set; }
    }
}