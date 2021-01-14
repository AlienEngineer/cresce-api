using System;
using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    public interface IAuthorizationFactory
    {
        IAuthorization Decode(string token);
        IAuthorization GetAuthorizedUser(User user, DateTime? dateTime = null);
        IAuthorization MakeUnauthorizedUser();
        IEmployeeAuthorization GetAuthorizedEmployee(IAuthorization user, string employeeId);
        IEmployeeAuthorization MakeUnauthorizedEmployee();
    }
}
