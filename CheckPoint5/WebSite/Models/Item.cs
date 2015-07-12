using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int? Cost { get; set; }
        public string Description { get; set; }
        public int? Number { get; set; }
    }
}