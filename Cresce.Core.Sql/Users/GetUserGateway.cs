using System.Threading.Tasks;
using Cresce.Core.Sql.GetEntities;
using Cresce.Core.Users;

namespace Cresce.Core.Sql.Users
{
    internal class GetUserGateway : IGetUserGateway
    {
        private readonly IGetEntityById<User> _getById;

        public GetUserGateway(IGetEntityById<User> getById)
        {
            _getById = getById;
        }

        public Task<User> GetUser(string user) =>  _getById.GetById(user);
    }
}
