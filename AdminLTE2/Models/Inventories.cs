using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AdminLTE2.Models
{
    //[Keyless]
    public class Inventories
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Products")]
        public int product_id { get; set; }
        public Products? Products { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Warehouses")]
        public int warehouse_id { get; set; }
        public Warehouses? Warehouses { get; set; }
        //[JsonPropertyName("quantity")]
        public int quantity { get; set; }
    }
}
