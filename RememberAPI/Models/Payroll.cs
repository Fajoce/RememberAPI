using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RememberAPI.Models
{
    public class Payroll
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string lastName { get; set; }
        [Required(ErrorMessage ="This field is required")]
        public double Salary { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Days { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DateTime PayDate  { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
