using System.ComponentModel.DataAnnotations;

namespace RememberAPI.DTOs
{
    public class PayrollDTO
    {
    [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string lastName { get; set; }
        public double salary { get; set; }
        public int days { get; set; }
        public int earnedSalary { get { return (int)(salary * days/30); } }
        public DateTime payDate  { get { return DateTime.Now; } }
        public int DepartmentId  { get; set; }     
      

    }
}
