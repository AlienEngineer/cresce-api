using System.Threading.Tasks;

namespace Cresce.Core.Authentication
{
    public interface ILoginService
    {
        Task<bool> AreCredentialsValid(Credentials credentials);
        Task<AuthorizedUser> ValidateCredentials(Credentials credentials);
    }
}
