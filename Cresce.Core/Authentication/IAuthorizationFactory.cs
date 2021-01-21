using System;
using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    public interface IAuthorizationFactory
    {
        IAuthorization Decode(string token);
        IAuthorization MakeAuthorization(User user, DateTime? dateTime = null);
        IAuthorization MakeExpiredAuthorization();
        IEmployeeAuthorization GetAuthorizedEmployee(IAuthorization user, string employeeId);
        IEmployeeAuthorization MakeExpiredEmployeeAuthorization();
    }
}
