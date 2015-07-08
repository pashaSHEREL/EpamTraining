using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
namespace DAL.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        [Required]
        public int? Cost { get; set; }
        public string Description { get; set; }
    }
}