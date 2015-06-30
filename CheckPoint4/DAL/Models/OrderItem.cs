namespace DAL.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int? Quantity { get; set; }
        public int? TotalCost { get; set; }
        public Item Item { get; set; }
    }
}