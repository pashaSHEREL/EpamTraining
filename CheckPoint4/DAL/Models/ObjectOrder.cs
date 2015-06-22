using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ObjectOrder
    {
        public int OrderId { get; set; }

        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }

        public int? CustomerId { get; set; }
    }
}
