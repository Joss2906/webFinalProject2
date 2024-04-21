using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AdminLTE2.Models
{
    public class Locations
    {
        [Key]
        public int location_id { get; set; }
        public string address { get; set; }
        public string? postal_code { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }

        [ForeignKey("Countries")]
        public string? country_id { get; set; }
        public Countries? Countries { get; set; }

    }
}
