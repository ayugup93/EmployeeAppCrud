using Employee.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure.DbEntities
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }
        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
