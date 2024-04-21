using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AdminLTE2.Models
{
    public class Countries
    {
        [Key]
        public string country_id { get; set; }
        public string country_name { get; set; }

        [ForeignKey("Regions")]
        public int? region_id { get; set; }
        public Regions? Regions { get; set; }
    }
}
