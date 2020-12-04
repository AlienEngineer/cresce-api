using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Cresce.Api.Controllers.Employees
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeController : ControllerBase
    {
        public Task<IEnumerable<Employee>> GetEmployees(string organization)
        {
            return null;
        }
    }
}
