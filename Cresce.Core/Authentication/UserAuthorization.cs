using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cresce.Core.Organizations;
using Cresce.Core.Users;

namespace Cresce.Core.Authentication
{
    internal class UserAuthorization : IAuthorization
    {
        private readonly JwtSecurityToken _token;
        private readonly IGetUserOrganizationsGateway _gateway;

        internal UserAuthorization(JwtSecurityToken token, IGetUserOrganizationsGateway gateway)
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
            EnsureIsNotExpired();
            return _token.Claims.FirstOrDefault(e => e.Type == type)
                   ?? new Claim("unknown", "");
        }

        public void EnsureIsNotExpired()
        {
            if (IsExpired)
            {
                throw new UnauthorizedException("Authorization has expired.");
            }
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
        int EmployeeId { get; }
        void EnsureIsValid();
    }

    internal class AuthorizedEmployee : UserAuthorization, IEmployeeAuthorization
    {
        internal AuthorizedEmployee(JwtSecurityToken token, IGetUserOrganizationsGateway gateway)
            : base(token, gateway)
        {
        }

        public int EmployeeId => int.Parse(GetClaim(ClaimTypes.UserData).Value);

        public void EnsureIsValid()
        {
            if (EmployeeId <= 0)
            {
                throw new UnauthorizedException("Authorization has expired.");
            }
        }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
