using Employee.Domain.Models;
using Employee.Services.Interfaces;
using Employee.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Controllers
{
    [Route("api/efcore/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService , ILogger<EmployeeController> logger)
        {
            this._employeeService = employeeService;
            this._logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Get() 
        {
           var emp = await _employeeService.GetAllEmployees();
            return Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeModel emp)
        {
            await _employeeService.CreateEmployee(emp);
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(int id, EmployeeModel employee)
        {
            _logger.LogInformation("Inside the Update Employee Method");
            try
            {
                var existingEmployee = await _employeeService.GetByIdAsync(id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Position = employee.Position;
                existingEmployee.Salary = employee.Salary;

                await _employeeService.UpdateEmployee(existingEmployee);
                _logger.LogInformation("Employee record Updated Successfully!");
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
                var existingEmployee = await _employeeService.GetByIdAsync(id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                await _employeeService.DeleteEmployee(id);
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
