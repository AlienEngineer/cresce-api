namespace Cresce.Api.Controllers.Authentications
{
    public record CredentialsDto
    {
        public string User { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}