using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AdminLTE2.Models
{
    public class Order_Items
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Orders")]
        public int order_id { get; set; }
        public Orders? Orders { get; set; }
        
        [Key, Column(Order = 2)]
        public int item_id { get; set; }

        
        [ForeignKey("Products")]
        public int product_id { get; set; }
        public Products? Products { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; } 
    }
}
