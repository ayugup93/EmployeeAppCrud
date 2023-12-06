namespace Employee.Domain.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }
        public double? Salary { get; set; }
    }
}
