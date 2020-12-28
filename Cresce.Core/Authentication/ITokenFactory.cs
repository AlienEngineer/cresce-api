using System;
using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    public interface ITokenFactory
    {
        AuthorizedUser Decode(string token);
        AuthorizedUser MakeToken(User user, DateTime? dateTime = null);
        AuthorizedUser MakeInvalidToken();
    }
}
