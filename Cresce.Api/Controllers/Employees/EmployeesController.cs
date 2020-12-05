using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Cresce.Api.Controllers.Employees
{
    [ApiController]
    [Route("api/v1/")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet("organization/{organization}/[controller]")]
        public Task<IEnumerable<Employee>> GetEmployees(AuthorizedUser user, string organization)
        {
            return _service.GetEmployees(user, organization);
        }
    }
}
