﻿namespace Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int? Quantity { get; set; }
    }
}