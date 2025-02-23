using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RememberAPI.DTOs;
using RememberAPI.Models;
using RememberAPI.PayrollData;

namespace RememberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly ILogger<PayrollController> _logger;
        private readonly ApplicationDbContext _context;
        public PayrollController(ILogger<PayrollController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _context = applicationDbContext;    
        }
        [HttpGet]
        public ActionResult<IEnumerable<Payroll>> GetList()
        {
            
            var  result = from d in _context.payrolls join a in _context.departments
                          on d.DepartmentId equals a.Id
                          select new 
                          {
                              d.Id,
                              d.Name,
                              d.lastName,
                              d.Salary,
                              d.Days,
                              d.DepartmentId,
                              a.DepartmentName
                              
                          };
            if (result == null)
            {
                _logger.LogError("There is not element in the list");
                return BadRequest();
            }
            else
            {
                _logger.LogInformation("Elements retrieving sucessfully");
                return Ok(result.ToList());
            }
        }

        [HttpGet("id", Name ="CreatePayroll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  ActionResult<Payroll> GetPayrollById(int id)
        {
            if (id == 0)
            {
                return NotFound();

            }
            //var result = PayrollStore.payrollList.FirstOrDefault(x => x.EmployeeId ==id);
            var result = from x in _context.payrolls join y in _context.departments
                         on x.DepartmentId equals y.Id
                         where x.Id == id select new
                         {
                             x.Id,
                            x.Name,
                             x.lastName,
                             x.Salary,
                             x.Days,
                             x.DepartmentId,
                             y.DepartmentName
                         };
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("lastName")]
        public IEnumerable<PayrollDTO> GetSalaryDesc(string lastName)
        {
            var result = from s in PayrollStore.payrollList
                         where s.lastName.Contains(lastName)
                         select s;
           
                return result.ToList().OrderByDescending(x => x.salary);
            


        }
        [HttpGet("between")]
        public IEnumerable<PayrollDTO> GetRangeSalary(string lastName)
        {
            var result = from s in PayrollStore.payrollList
                         where s.salary >= 2000000 & s.salary <=3000000 & s.lastName == lastName
                         select s;

            return result.ToList().OrderByDescending(x => x.salary);

        }
        [HttpGet("Average")]
        public double GetRangeSalary2()
        {
            var result = (from s in PayrollStore.payrollList
                          select s.salary)
                         .Average();                    
                        

            return result;

        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InsertPaymentDTO> PostPayroll([FromBody] InsertPaymentDTO payroll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           /* if(PayrollStore.payrollList.FirstOrDefault(x=> x.Code == payroll.Code) != null)
            {
                ModelState.AddModelError("Error", "This code already exists!");
                return BadRequest(ModelState);
            }*/
            if (payroll == null)
            {
                return BadRequest(payroll);
            }
           /* if (payroll.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }*/
            // payroll.EmployeeId = PayrollStore.payrollList.OrderByDescending(x => x.EmployeeId).FirstOrDefault().EmployeeId + 1;
            // PayrollStore.payrollList.Add(payroll);
            Payroll payment = new()
            {
                Name = payroll.Name,
                lastName = payroll.lastName,
                Salary = payroll.salary,
                Days = payroll.days,
                PayDate = DateTime.Now,
                DepartmentId = payroll.DepartmentId
            };
           _context.payrolls.Add(payment);
            _context.SaveChanges(); 
            _logger.LogInformation("Payroll added into the database with code: " + payment.Id);
            return CreatedAtRoute("CreatePayroll", new {id = payment.Id, payment});
        }
        //Delete
        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeletePayroll(int id)
        {
            if(id== 0) { return BadRequest(); }
            //var x = PayrollStore.payrollList.FirstOrDefault(a=> a.EmployeeId == id);
            var x = _context.payrolls.FirstOrDefault(a => a.Id == id);
            if (x == null)
            {
                return NotFound();
            }
            //PayrollStore.payrollList.Remove(x);
            _context.payrolls.Remove(x);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPut("id")]
        public ActionResult<PayrollDTO> PutPayroll(int id, [FromBody] PayrollDTO payroll)
        {
           /* if(payroll == null || id != payroll.EmployeeId)
            {
                return BadRequest();
            }*/
           // var x = _context.payrolls.FirstOrDefault(a=> a.Id == id);
           // x.salary = payroll.salary;
            //x.days = payroll.days;
            //x.Department = payroll.Department;
            Payroll payment = new()
            {
                Id = id,
                Name = payroll.Name,
                lastName = payroll.lastName,
                Salary = payroll.salary,
                Days = payroll.days,
                PayDate = DateTime.Now,
                DepartmentId = payroll.DepartmentId
            };
            _context.payrolls.Update(payment);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("id")]
        public ActionResult<PayrollDTO> PartialPatchPayroll(int id, JsonPatchDocument<PayrollDTO> patchpayroll)
        {
            if (patchpayroll == null || id == 0)
            {
                return BadRequest();
            }
            //var x = PayrollStore.payrollList.FirstOrDefault(a => a.Id == id);
            var x = _context.payrolls.AsNoTracking().FirstOrDefault(a => a.Id == id);
            /*patchpayroll.ApplyTo(x, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }*/
            PayrollDTO payment = new()
            {
                Id = x.Id,
                Name = x.Name,
                lastName = x.lastName,
                salary = x.Salary,
                days = x.Days,
                DepartmentId = x.DepartmentId
            };
            if (payment == null) return BadRequest();
            patchpayroll.ApplyTo(payment, ModelState);
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
             }

            Payroll model = new()
            {
                Id = payment.Id,
                Name = payment.Name,
                lastName = payment.lastName,
                Salary = payment.salary,
                Days = payment.days,
                DepartmentId = payment.DepartmentId
            };

            _context.payrolls.Update(model);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
