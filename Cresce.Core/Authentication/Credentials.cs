using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    public record Credentials
    {
        public Credentials(string userId, string password)
        {
            UserId = userId;
            Password = password;
        }

        public string UserId { get; }
        private string Password { get; }

        internal bool Verify(User user)
        {
            return GetCredentialsUser() == user;
        }

        private User GetCredentialsUser()
        {
            return new AdminUser
            {
                Id = UserId,
                Password = Password
            };
        }
    }
}
