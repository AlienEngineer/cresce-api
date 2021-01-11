using System.Linq;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Organizations;
using Microsoft.AspNetCore.Mvc;

namespace Cresce.Api.Controllers.Organizations
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _service;

        public OrganizationsController(IOrganizationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(AuthorizedUser user)
        {
            var organizations = await _service.GetOrganizations(user);

            return organizations.Any()
                ? (IActionResult) Ok(organizations)
                : NotFound();
        }
    }
}
