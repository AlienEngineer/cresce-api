using Cresce.Core.Users;

namespace Cresce.Core.Sql.Users
{
    internal class UserModel
    {
        public string Id { get; set; }

        public User ToUser()
        {
            if (Id == null)
            {
                return new UnknownUser();
            }
            return new AdminUser
            {
                Id = Id
            };
        }
    }
}
