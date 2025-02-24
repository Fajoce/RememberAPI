using RememberAPI.DTOs;

namespace RememberAPI.PayrollData
{
    public static class PayrollStore
    {
        public static List<PayrollDTO> payrollList = new List<PayrollDTO>
        {
             new PayrollDTO {  Id = 1, Name = "Jocelyn", lastName ="Ortiz", Salary = 1320000, Days=30},
               new PayrollDTO { Id=2, Name = "Nicolle", lastName ="Ortiz", Salary = 2900000, Days = 28},
               new PayrollDTO { Id=3, Name = "Gregoria", lastName ="Charris", Salary = 3000000, Days =30},
               new PayrollDTO { Id=4, Name = "Elvis", lastName ="Ortiz", Salary = 2500000, Days =29},
               new PayrollDTO { Id=5, Name = "Ricardo", lastName ="Acosta", Salary = 1320000, Days=30},
               new PayrollDTO { Id=6, Name = "Armando", lastName ="Acosta", Salary = 2950000, Days = 28},
               new PayrollDTO { Id=7, Name = "Anais", lastName ="Acosta", Salary = 3300000, Days =30},
               new PayrollDTO { Id=8,Name = "Rosiris", lastName ="Ortiz", Salary = 1500000, Days =29}
        };
    }
}
