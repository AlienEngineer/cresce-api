using System;
using System.Threading.Tasks;
using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    public interface IAuthorization
    {
        bool IsExpired { get; }
        string UserId { get; }
        string Role { get; }
        DateTime ExpirationDate { get; }
        Task EnsureCanAccessOrganization(string organizationId);
        User ToUser();
        void EnsureIsNotExpired();
    }
}
