using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ObjectItem
    {
        public int ItemId { get; set; }

        public string Name { get; set; }

        public int? Cost { get; set; }

        public string Description { get; set; }
    }
}
