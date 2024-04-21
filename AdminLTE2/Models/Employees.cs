using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AdminLTE2.Models
{
    public class Employees
    {
        [Key]
        public int employee_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public DateTime? hire_date { get; set; }

        [ForeignKey("Managers")]
        public int? manager_id { get; set; }
        public Employees? Managers { get; set; }
        
        public string job_title { get; set;}

    }
}
