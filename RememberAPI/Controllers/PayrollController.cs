using AutoMapper;
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
        private readonly IMapper _imapper;
        public PayrollController(ILogger<PayrollController> logger, ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _logger = logger;
            _context = applicationDbContext;   
            _imapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayrollDTO>>> GetList()
        {
            
            var  result =  from d in _context.payrolls join a in _context.departments
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
                return Ok(await result.ToListAsync());
            }
        }

        [HttpGet("id", Name ="CreatePayroll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PayrollDTO>> GetPayrollById(int id)
        {
            if (id == 0)
            {
                return NotFound();

            }
            //var result = PayrollStore.payrollList.FirstOrDefault(x => x.EmployeeId ==id);
            var result = await (from x in _context.payrolls join y in _context.departments
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
                         }).FirstOrDefaultAsync();
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
           
                return result.ToList().OrderByDescending(x => x.Salary);

        }

        [HttpGet("Aggregate")]
        public async Task<List<AgreggateDTO>> Aggregate()
        {
            var result = from s in _context.payrolls join d in _context.departments
                         on s.DepartmentId equals d.Id
                         group s by s.DepartmentId into g
                         
                         select new AgreggateDTO
                         {                  
                             DepartmentId = g.Key,
                             Quantity = g.Count(),
                             Average = g.Average(x=> x.Salary),
                             MaxSalary = g.Max(x=> x.Salary),
                             MinSalary = g.Min(x=> x.Salary)
                            
                         };
            
            return await result.ToListAsync();
        }
        [HttpGet("between")]
        public IEnumerable<PayrollDTO> GetRangeSalary(string lastName)
        {
            var result = from s in PayrollStore.payrollList
                         where s.Salary >= 2000000 & s.Salary <=3000000 & s.lastName == lastName
                         select s;

            return result.ToList().OrderByDescending(x => x.Salary);

        }
       
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InsertPaymentDTO>> PostPayroll([FromBody] InsertPaymentDTO insertPayment)
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
            if (insertPayment == null)
            {
                return BadRequest(insertPayment);
            }
           /* if (payroll.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }*/
            // payroll.EmployeeId = PayrollStore.payrollList.OrderByDescending(x => x.EmployeeId).FirstOrDefault().EmployeeId + 1;
            // PayrollStore.payrollList.Add(payroll);

            var payment = _imapper.Map<Payroll>(insertPayment);
           
            /*Payroll payment = new()
            {
                Name = payroll.Name,
                lastName = payroll.lastName,
                Salary = payroll.salary,
                Days = payroll.days,
                PayDate = DateTime.Now,
                DepartmentId = payroll.DepartmentId
            };*/
           _context.payrolls.AddAsync(payment);
            await _context.SaveChangesAsync(); 
            _logger.LogInformation("Payroll added into the database with code: " + payment.Id);
            return CreatedAtRoute("CreatePayroll", new {id = payment.Id, payment});
        }
        //Delete
        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePayroll(int id)
        {
            if(id== 0) { return BadRequest(); }
            //var x = PayrollStore.payrollList.FirstOrDefault(a=> a.EmployeeId == id);
           var x = await _context.payrolls.FirstOrDefaultAsync(a => a.Id == id);
        
            if (x == null)
            {
                return NotFound();
            }
            //PayrollStore.payrollList.Remove(x);
            _context.payrolls.Remove(x);
           await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("id")]
        public async Task<ActionResult<UpdatePayrollDTO>> PutPayroll(int id, [FromBody] UpdatePayrollDTO updatePayrollDTO)
        {
           /* if(payroll == null || id != payroll.EmployeeId)
            {
                return BadRequest();
            }*/
           // var x = _context.payrolls.FirstOrDefault(a=> a.Id == id);
           // x.salary = payroll.salary;
            //x.days = payroll.days;
            //x.Department = payroll.Department;

            var payment = _imapper.Map<Payroll>(updatePayrollDTO);
           /* Payroll payment = new()
            {
                Id = id,
                Name = payroll.Name,
                lastName = payroll.lastName,
                Salary = payroll.Salary,
                Days = payroll.Days,
                PayDate = DateTime.Now,
                DepartmentId = payroll.DepartmentId
            };*/
            _context.payrolls.Update(payment);
           await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("id")]
        public async Task<ActionResult<PayrollDTO>> PartialPatchPayroll(int id, JsonPatchDocument<UpdatePayrollDTO> patchpayroll)
        {
            if (patchpayroll == null || id == 0)
            {
                return BadRequest();
            }
            //var x = PayrollStore.payrollList.FirstOrDefault(a => a.Id == id);
            var x = await _context.payrolls.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            /*patchpayroll.ApplyTo(x, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }*/
            UpdatePayrollDTO payment = _imapper.Map<UpdatePayrollDTO>(x);
           
            /*PayrollDTO payment = new()
            {
                Id = x.Id,
                Name = x.Name,
                lastName = x.lastName,
                Salary = x.Salary,
                Days = x.Days,
                DepartmentId = x.DepartmentId
            };*/
            if (payment == null) return BadRequest();
            patchpayroll.ApplyTo(payment, ModelState);
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
             }

            Payroll model = _imapper.Map<Payroll>(payment);
           
            /*Payroll model = new()
            {
                Id = payment.Id,
                Name = payment.Name,
                lastName = payment.lastName,
                Salary = payment.Salary,
                Days = payment.Days,
                DepartmentId = payment.DepartmentId
            };*/

            _context.payrolls.Update(model);
           await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
