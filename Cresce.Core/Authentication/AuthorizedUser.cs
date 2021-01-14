using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cresce.Core.Organizations;
using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    internal class AuthorizedUser : IAuthorization
    {
        private readonly JwtSecurityToken _token;
        private readonly IGetUserOrganizationsGateway _gateway;

        internal AuthorizedUser(JwtSecurityToken token, IGetUserOrganizationsGateway gateway)
        {
            _token = token;
            _gateway = gateway;
        }

        public bool IsExpired => _token.ValidTo < DateTime.UtcNow.AddSeconds(5);
        public string UserId => GetClaim("unique_name").Value;
        public string Role => GetClaim("role").Value;
        public DateTime ExpirationDate => _token.ValidTo;

        protected Claim GetClaim(string type)
        {
            EnsureTokenIsStillValid();
            return _token.Claims.FirstOrDefault(e => e.Type == type)
                   ?? new Claim("unknown", "");
        }

        private void EnsureTokenIsStillValid()
        {
            if (!IsExpired) return;
            throw new UnauthorizedException("Unable to access resource, token expired.");
        }

        public override string ToString() => new JwtSecurityTokenHandler().WriteToken(_token);

        public async Task EnsureCanAccessOrganization(string organizationId)
        {
            var organizations = await _gateway.GetOrganizations(UserId);
            if (organizations.All(e => e.Name != organizationId))
            {
                throw new UnauthorizedException($"[{UserId}] doesn't have access to [{organizationId}]");
            }
        }

        public User ToUser() => new AdminUser {Id = UserId};
    }

    public interface IEmployeeAuthorization : IAuthorization
    {
        string EmployeeId { get; }
    }

    internal class AuthorizedEmployee : AuthorizedUser, IEmployeeAuthorization
    {
        internal AuthorizedEmployee(JwtSecurityToken token, IGetUserOrganizationsGateway gateway)
            : base(token, gateway)
        {
        }

        public string EmployeeId => GetClaim(ClaimTypes.UserData).Value;
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
