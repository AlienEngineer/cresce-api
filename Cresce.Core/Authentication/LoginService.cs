using System.Threading.Tasks;
using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    internal class LoginService : ILoginService
    {
        private readonly IGetUserGateway _gateway;
        private readonly IAuthorizationFactory _authorizationFactory;

        public LoginService(IGetUserGateway gateway, IAuthorizationFactory authorizationFactory)
        {
            _gateway = gateway;
            _authorizationFactory = authorizationFactory;
        }

        public async Task<bool> AreCredentialsValid(Credentials credentials)
        {
            return credentials.Verify(await _gateway.GetUser(credentials.UserId));
        }

        public async Task<IAuthorization> ValidateCredentials(Credentials credentials)
        {
            var user = await _gateway.GetUser(credentials.UserId);
            return credentials.Verify(user)
                    ? _authorizationFactory.GetAuthorizedUser(user)
                    : _authorizationFactory.MakeUnauthorizedUser();
        }
    }
}
