namespace DAL.Models
{
    public class OrderItem
    {
        public Order Order { get; set; }
        public Item Item { get; set; }
        public int? Quantity { get; set; }
        public int? TotalCost { get; set; }
        
    }
}