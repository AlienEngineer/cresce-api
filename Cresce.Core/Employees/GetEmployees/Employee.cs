using Cresce.Core.Employees.EmployeeValidation;

namespace Cresce.Core.Employees.GetEmployees
{
    public record Employee
    {
        public int Id { get; init; } = -1;
        public string Name { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public Image Image { get; init; } = new();
        public string Pin { get; init; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string OrganizationId { get; set; } = string.Empty;

        public bool Verify(EmployeePin employeePin)
        {
            return employeePin.Pin == Pin;
        }
    }
}
