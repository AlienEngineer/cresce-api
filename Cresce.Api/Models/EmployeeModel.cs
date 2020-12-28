using Cresce.Core.Employees;

namespace Cresce.Api.Models
{
    public record EmployeeModel
    {
        public EmployeeModel(Employee employee)
        {
            Name = employee.Name;
            Title = employee.Title;
            Image = employee.Image.ToBase64();
        }

        public EmployeeModel() { }

        public string Name { get; init; }
        public string Image { get; init; }
        public string Title { get; init; }
    }
}
