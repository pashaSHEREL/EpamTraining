﻿namespace DAL.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int? Cost { get; set; }
        public string Description { get; set; }
    }
}