using System;

namespace Bll
{
    public interface IObjectFromCsv
    {
        DateTime Date { get; set; }
        int CustomerId { get; set; }
        int ItemId { get; set; }
        int TotalCost { get; set; }
        int Quantity { get; set; }
        int OrderNumber { get; set; }
    }
}