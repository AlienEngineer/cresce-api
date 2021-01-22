using System.Threading.Tasks;
using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    internal class LoginService : ILoginService
    {
        private readonly IGetEntityByIdGateway<User> _gateway;
        private readonly IAuthorizationFactory _authorizationFactory;

        public LoginService(
            IGetEntityByIdGateway<User> gateway,
            IAuthorizationFactory authorizationFactory
        )
        {
            _gateway = gateway;
            _authorizationFactory = authorizationFactory;
        }

        public async Task<bool> AreCredentialsValid(Credentials credentials)
        {
            return credentials.Verify(await _gateway.GetById(credentials.UserId));
        }

        public async Task<IAuthorization> ValidateCredentials(Credentials credentials)
        {
            var user = await _gateway.GetById(credentials.UserId);
            return credentials.Verify(user)
                ? _authorizationFactory.MakeAuthorization(user)
                : _authorizationFactory.MakeExpiredAuthorization();
        }
    }
}