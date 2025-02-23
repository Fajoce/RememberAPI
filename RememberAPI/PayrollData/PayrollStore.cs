using RememberAPI.DTOs;

namespace RememberAPI.PayrollData
{
    public static class PayrollStore
    {
        public static List<PayrollDTO> payrollList = new List<PayrollDTO>
        {
             new PayrollDTO {  Id = 1, Name = "Jocelyn", lastName ="Ortiz", salary = 1320000, days=30},
               new PayrollDTO { Id=2, Name = "Nicolle", lastName ="Ortiz", salary = 2900000, days = 28},
               new PayrollDTO { Id=3, Name = "Gregoria", lastName ="Charris", salary = 3000000, days =30},
               new PayrollDTO { Id=4, Name = "Elvis", lastName ="Ortiz", salary = 2500000, days =29},
               new PayrollDTO { Id=5, Name = "Ricardo", lastName ="Acosta", salary = 1320000, days=30},
               new PayrollDTO { Id=6, Name = "Armando", lastName ="Acosta", salary = 2950000, days = 28},
               new PayrollDTO { Id=7, Name = "Anais", lastName ="Acosta", salary = 3300000, days =30},
               new PayrollDTO { Id=8,Name = "Rosiris", lastName ="Ortiz", salary = 1500000, days =29}
        };
    }
}
