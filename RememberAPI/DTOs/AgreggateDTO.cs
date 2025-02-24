namespace RememberAPI.DTOs
{
    public class AgreggateDTO
    {
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public double MaxSalary { get; set; }
        public double MinSalary { get; set; }
        public int Quantity { get; set; }
        public double Average { get; set; }
    }
}
