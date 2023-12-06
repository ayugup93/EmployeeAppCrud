using Employee.Domain.Models;
using Employee.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Controllers
{
    [Route("api/dapper/[controller]")]
    [ApiController]
    public class EmployeeDapperController : ControllerBase
    {
        private readonly IEmployeeDapperService _employeeDapperService;
        private readonly ILogger<EmployeeDapperController> _logger;

        public EmployeeDapperController(IEmployeeDapperService employeeDapperService , ILogger<EmployeeDapperController> logger)
        {
            this._employeeDapperService = employeeDapperService;
            this._logger = logger;
        }
         
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var emp = await _employeeDapperService.GetAllAsync();
            return Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeModel emp)
        {
            await _employeeDapperService.CreateAsync(emp);
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(int id, EmployeeModel employee)
        {
            _logger.LogInformation("Inside the Update Employee Method");
            try
            {
                var existingEmployee = await _employeeDapperService.GetByIdAsync(id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Position = employee.Position;
                existingEmployee.Salary = employee.Salary;

                await _employeeDapperService.UpdateAsync(existingEmployee);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                _logger.LogError("An error occurred while updating the employee:", ex.Message);
                return StatusCode(500, "An error occurred while updating the employee: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            _logger.LogInformation("Inside the Delete Employee Method");
            try
            {
                var existingEmployee = await _employeeDapperService.GetByIdAsync(id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                await _employeeDapperService.DeleteAsync(id);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                _logger.LogError("An error occurred while deleting the employee:", ex.Message);
                return StatusCode(500, "An error occurred while deleting the employee: " + ex.Message);
            }
        }
    }
}
