using Employee.Domain.Models;

namespace Employee.Services.Interfaces
{
    public interface IEmployeeDapperService
    {
        Task<int> CreateAsync(EmployeeModel employee);
        Task<List<EmployeeModel>> GetAllAsync();
        Task<EmployeeModel> GetByIdAsync(int id);
        Task<int> UpdateAsync(EmployeeModel employee);
        Task<int> DeleteAsync(int id);
    }
}