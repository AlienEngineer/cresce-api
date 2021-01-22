namespace Cresce.Api.Controllers.Employees
{
    public record EmployeeLoginResultDto
    {
        public string Token { get; init; } = string.Empty;
    }
}