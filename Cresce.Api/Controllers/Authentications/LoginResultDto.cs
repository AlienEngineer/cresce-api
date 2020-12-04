namespace Cresce.Api.Controllers.Authentications
{
    public record LoginResultDto
    {
        public string OrganizationUrl { get; set; }
        public string Token { get; set; }
    }
}
