using System;
using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    public interface IAuthorizedUserFactory
    {
        AuthorizedUser Decode(string token);
        AuthorizedUser GetAuthorizedUser(User user, DateTime? dateTime = null);
        AuthorizedUser MakeUnauthorizedUser();
        AuthorizedEmployee GetAuthorizedEmployee(AuthorizedUser user, string employeeId);
        AuthorizedEmployee makeUnauthorizedEmployee();
    }
}
