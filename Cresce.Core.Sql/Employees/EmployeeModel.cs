using Cresce.Core.Employees.GetEmployees;

namespace Cresce.Core.Sql.Employees
{
    internal class EmployeeModel : IUnwrap<Employee>, IWrap<Employee>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public string OrganizationId { get; set; }
        public string Pin { get; set; }

        public Employee Unwrap()
        {
            return new Employee
            {
                Name = Id,
                Title = Title,
                Image = new Image(Image),
                Pin = Pin
            };
        }

        public void Wrap(Employee entity)
        {
            throw new System.NotImplementedException();
        }
    }
}