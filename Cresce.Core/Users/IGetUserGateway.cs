using System.Threading.Tasks;

namespace Cresce.Core.Users
{
    public interface IGetUserGateway
    {
        Task<User> GetUser(string user);
    }
}
