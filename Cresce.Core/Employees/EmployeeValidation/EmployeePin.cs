namespace Cresce.Core.Employees.EmployeeValidation
{
    public record EmployeePin
    {
        public int EmployeeId { get; init; } = -1;
        public string Pin { get; init; } = string.Empty;
    }
}