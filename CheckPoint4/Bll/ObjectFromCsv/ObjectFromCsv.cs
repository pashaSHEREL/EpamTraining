using System;

namespace Bll
{
    public class ObjectFromCsv : IObjectFromCsv
    {
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public int TotalCost { get; set; }
        public int Quantity { get; set; }
        public int OrderNumber { get; set; }
    }
}