using Employee.Domain.Models;

namespace Employee.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetAllEmployees();
        Task CreateEmployee(EmployeeModel emp);
        Task UpdateEmployee(EmployeeModel emp);
        Task DeleteEmployee(int id);
        Task<EmployeeModel> GetByIdAsync(int id);
    }
}