using System.Threading.Tasks;
using Cresce.Core.Users;

namespace Cresce.Core.Sql.Users
{
    internal class GetUserGateway : IGetUserGateway
    {
        private readonly CresceContext _context;

        public GetUserGateway(CresceContext context) => _context = context;

        public async Task<User> GetUser(string user) => (await GetUserModel(user)).ToUser();

        private async Task<UserModel> GetUserModel(string user) =>
            await _context.FindAsync<UserModel>(user) ?? new UserModel();
    }
}
