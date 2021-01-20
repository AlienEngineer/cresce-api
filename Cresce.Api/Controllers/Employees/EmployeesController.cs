using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cresce.Api.Controllers.Authentications;
using Cresce.Api.Models;
using Cresce.Core.Authentication;
using Cresce.Core.Employees;
using Cresce.Core.Employees.EmployeeValidation;
using Microsoft.AspNetCore.Mvc;

namespace Cresce.Api.Controllers.Employees
{
    [ApiController]
    [Route("api/v1/")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service) => _service = service;

        [HttpGet("organization/{organization}/[controller]")]
        public async Task<IEnumerable<EmployeeModel>> GetEmployees([FromHeader] IAuthorization user, string organization)
        {
            return (await _service.GetEmployees(user, organization))
                .Select(employee => new EmployeeModel(employee));
        }

        [HttpPost("[controller]/")]
        public async Task<IActionResult> ValidatePin([FromHeader] IAuthorization user, [FromBody] EmployeePin pin)
        {
            var authorization = await _service.ValidatePin(user, pin);

            if (authorization.IsExpired) return Unauthorized();

            return new OkObjectResult(new EmployeeLoginResultDto
            {
                Token = authorization.ToString()!
            });
        }


    }
}
