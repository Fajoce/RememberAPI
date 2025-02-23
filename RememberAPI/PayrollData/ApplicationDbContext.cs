using Microsoft.EntityFrameworkCore;
using RememberAPI.Models;

namespace RememberAPI.PayrollData
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<Department> departments { get; set; }
        public DbSet<Payroll> payrolls { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().HasData(
            new Department
            {
                Id = 1,
                DepartmentName = "Gerencia"
            },
             new Department
             {
                 Id = 2,
                 DepartmentName = "Ventas"
             },
              new Department
              {
                  Id = 3,
                  DepartmentName = "Produccion"
              }
            );
    }
    }
}
