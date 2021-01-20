namespace Cresce.Api.Controllers.Authentications
{
    public record LoginResultDto
    {
        public string OrganizationUrl { get; init; }= string.Empty;
        public string Token { get; init; }= string.Empty;
    }
}
