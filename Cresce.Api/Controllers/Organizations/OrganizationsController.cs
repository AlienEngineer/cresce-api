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
        private readonly ITokenFactory _tokenFactory;

        public OrganizationsController(IOrganizationService service, ITokenFactory tokenFactory)
        {
            _service = service;
            _tokenFactory = tokenFactory;
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
