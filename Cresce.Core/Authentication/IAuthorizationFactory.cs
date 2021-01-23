using System;
using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    public interface IAuthorizationFactory
    {
        IAuthorization DecodeAuthorization(string token);
        IEmployeeAuthorization DecodeEmployeeAuthorization(string token);
        IAuthorization MakeAuthorization(User user, DateTime? dateTime = null);
        IAuthorization MakeExpiredAuthorization();
        IEmployeeAuthorization GetAuthorizedEmployee(IAuthorization user, int employeeId);
        IEmployeeAuthorization MakeExpiredEmployeeAuthorization();
    }
}