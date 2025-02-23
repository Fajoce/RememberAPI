using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RememberAPI.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="This field is required")]
        [StringLength(50)]
        public string DepartmentName { get; set; }
        [JsonIgnore]
        public ICollection<Payroll> Payrolls { get; }= new List<Payroll>();
    }
}
