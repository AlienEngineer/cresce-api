
namespace Cresce.Api.Controllers.Authentications
{
    public record CredentialsDto
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}

