﻿using System.ComponentModel.DataAnnotations;

namespace RememberAPI.DTOs
{
    public class PayrollDTO
    {
       
        public int Id { get; set; }
      
        public string Name { get; set; }
      
        public string lastName { get; set; }
      
        public double Salary { get; set; }
     
        public int Days { get; set; }
      
        public DateTime PayDate { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }



    }
}
