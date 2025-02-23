namespace RememberAPI.DTOs
{
    public class InsertPaymentDTO
    {
       
        public string Name { get; set; }
        public string lastName { get; set; }
        public double salary { get; set; }
        public int days { get; set; }
        public DateTime paymentDate { get; set; }
        public int DepartmentId { get; set; }
    }
}
