using Cresce.Core.Users;

namespace Cresce.Core.Sql.Users
{
    internal class UserModel : IUnwrap<User>
    {
        public string Id { get; set; }
        public string Password { get; set; }

        public User Unwrap()
        {
            if (Id == null)
            {
                return new UnknownUser();
            }

            return new AdminUser
            {
                Id = Id,
                Password = Password
            };
        }
    }
}
