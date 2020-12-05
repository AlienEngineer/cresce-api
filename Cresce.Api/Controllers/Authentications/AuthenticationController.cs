using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Cresce.Api.Controllers.Authentications
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthenticationController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(CredentialsDto credentials)
        {
            var user = await _loginService.ValidateCredentials(new Credentials(credentials.User, credentials.Password));

            if (user.IsExpired) return Unauthorized();

            return new OkObjectResult(new LoginResultDto
            {
                OrganizationUrl = $"api/v1/organizations/",
                Token = user.ToString()
            });
        }
    }

}
