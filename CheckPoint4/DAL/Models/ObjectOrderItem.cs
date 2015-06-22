using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Models
{
    public class ObjectOrderItem
    {
        public int OrderId { get; set; }

        public int ItemId { get; set; }

        public int? Quantity { get; set; }
    }
}
