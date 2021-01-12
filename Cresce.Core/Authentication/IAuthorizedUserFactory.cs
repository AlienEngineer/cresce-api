using System;
using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    public interface IAuthorizedUserFactory
    {
        AuthorizedUser Decode(string token);
        AuthorizedUser GetAuthorizedUser(User user, DateTime? dateTime = null);
        AuthorizedUser MakeInvalidToken();
    }
}
