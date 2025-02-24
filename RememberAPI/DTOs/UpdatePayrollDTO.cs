namespace RememberAPI.DTOs
{
    public class UpdatePayrollDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string lastName { get; set; }
        public double Salary { get; set; }
        public int Days { get; set; }
        public DateTime PayDate { get; set; }
        public int DepartmentId { get; set; }
    }
}
