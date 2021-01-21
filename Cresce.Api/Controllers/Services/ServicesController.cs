using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cresce.Api.Models;
using Cresce.Core.Authentication;
using Cresce.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cresce.Api.Controllers.Services
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceServices _service;

        public ServicesController(IServiceServices service) => _service = service;

        [HttpGet]
        public async Task<IEnumerable<ServiceModel>> GetServices([FromHeader] IEmployeeAuthorization authorization)
        {
            return (await _service.GetServices(authorization))
                .Select(employee => new ServiceModel(employee));
        }

    }
}
