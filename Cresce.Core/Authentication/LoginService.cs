using System.Threading.Tasks;
using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    internal class LoginService : ILoginService
    {
        private readonly IGetUserGateway _gateway;
        private readonly ITokenFactory _tokenFactory;

        public LoginService(IGetUserGateway gateway, ITokenFactory tokenFactory)
        {
            _gateway = gateway;
            _tokenFactory = tokenFactory;
        }

        public async Task<bool> AreCredentialsValid(Credentials credentials)
        {
            return credentials.Verify(await _gateway.GetUser(credentials.UserId));
        }

        public async Task<AuthorizedUser> ValidateCredentials(Credentials credentials)
        {
            var user = await _gateway.GetUser(credentials.UserId);
            return credentials.Verify(user)
                    ? _tokenFactory.MakeToken(user)
                    : _tokenFactory.MakeInvalidToken();
        }
    }
}
