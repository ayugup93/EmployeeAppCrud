using Dapper;
using Employee.Domain.Models;
using Employee.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace Employee.Services.Services
{
    public  class EmployeeDapperService : IEmployeeDapperService
    {
        private readonly IConfiguration _configuration;

        public EmployeeDapperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_configuration.GetConnectionString("EmployeesDBConnection"));
        }

        public async Task<int> CreateAsync(EmployeeModel employee)
        {
            using var connection = CreateConnection();
            const string query = "INSERT INTO employees (Id , FirstName, LastName, Position, Salary) VALUES (@Id , @FirstName, @LastName, @Position, @Salary)";
            return await connection.ExecuteAsync(query, employee);
        }

        public async Task<List<EmployeeModel>> GetAllAsync()
        {
            using var connection = CreateConnection();
            const string query = "SELECT * FROM employees";
            return (await connection.QueryAsync<EmployeeModel>(query)).ToList();
        }

        public async Task<EmployeeModel> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            const string query = "SELECT * FROM employees WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<EmployeeModel>(query, new { Id = id });
        }

        public async Task<int> UpdateAsync(EmployeeModel employee)
        {
            using var connection = CreateConnection();
            const string query = "UPDATE employees SET FirstName = @FirstName, LastName = @LastName, Position = @Position, Salary = @Salary WHERE Id = @Id";
            return await connection.ExecuteAsync(query, employee);
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            const string query = "DELETE FROM employees WHERE Id = @Id";
            return await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
