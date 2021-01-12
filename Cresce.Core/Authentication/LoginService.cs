using System.Threading.Tasks;
using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    internal class LoginService : ILoginService
    {
        private readonly IGetUserGateway _gateway;
        private readonly IAuthorizedUserFactory _authorizedUserFactory;

        public LoginService(IGetUserGateway gateway, IAuthorizedUserFactory authorizedUserFactory)
        {
            _gateway = gateway;
            _authorizedUserFactory = authorizedUserFactory;
        }

        public async Task<bool> AreCredentialsValid(Credentials credentials)
        {
            return credentials.Verify(await _gateway.GetUser(credentials.UserId));
        }

        public async Task<AuthorizedUser> ValidateCredentials(Credentials credentials)
        {
            var user = await _gateway.GetUser(credentials.UserId);
            return credentials.Verify(user)
                    ? _authorizedUserFactory.GetAuthorizedUser(user)
                    : _authorizedUserFactory.MakeInvalidToken();
        }
    }
}
