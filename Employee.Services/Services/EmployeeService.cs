using Employee.Domain.Models;
using Employee.Infrastructure.DbEntities;
using Employee.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Employee.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IServiceScopeFactory factory;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IServiceScopeFactory factory, ILogger<EmployeeService> logger)
        {
            this.factory = factory;
            this._logger = logger;
        }

        public async Task<List<EmployeeModel>> GetAllEmployees()
        {
            _logger.LogInformation("Inside the Get Employess method");
            using var scope = factory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
            
            try
            {
                return context.Employees.ToList();
            }

            catch (Exception ex)
            {
                _logger.LogError("Error Occured during getting Employees :{Message}", ex.Message);
                throw new ApplicationException("Unable to get List of Employees", ex);
            }
        }

        public async Task CreateEmployee(EmployeeModel emp)
        {
            using var scope = factory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();

            try
            {
                context.Employees.Add(emp);
               await context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the employee.",ex);
            }
        }

        public async Task UpdateEmployee(EmployeeModel emp)
        {
            using var scope = factory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();

            try {
                context.Entry(emp).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

            catch(Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the employee.",ex);
            }
        }

        public async Task DeleteEmployee(int id)
        {
            using var scope = factory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();

            try
            {
                var employee = context.Employees.Find(id);
                if (employee != null)
                {
                    context.Employees.Remove(employee);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the employee.", ex);
            }
        }

        public async Task<EmployeeModel> GetByIdAsync(int id)
        {
            using var scope = factory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();

            try
            {
                return await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the employee by ID.", ex);
            }
        }
        }
}
