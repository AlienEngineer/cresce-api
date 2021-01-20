namespace Cresce.Core.Employees.EmployeeValidation
{
    public record EmployeePin
    {
        public string EmployeeId { get; init; } = string.Empty;
        public string Pin { get; init; } = string.Empty;
    }
}
