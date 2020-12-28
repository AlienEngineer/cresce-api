using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cresce.Core.Organizations;
using Cresce.Core.Users;
using Microsoft.IdentityModel.Tokens;

namespace Cresce.Core.Authentication
{
    internal class TokenFactory : ITokenFactory
    {

        private readonly IGetUserOrganizationsGateway _gateway;

        public TokenFactory(IGetUserOrganizationsGateway gateway)
        {
            _gateway = gateway;
        }

        public AuthorizedUser Decode(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            return new AuthorizedUser(
                tokenHandler.CanReadToken(token)
                    ? tokenHandler.ReadJwtToken(token)
                    : new JwtSecurityToken(),
                _gateway
            );
        }

        public AuthorizedUser MakeToken(User user, DateTime? dateTime = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(MakeDescriptor(user, dateTime));
            return new AuthorizedUser((JwtSecurityToken) token, _gateway);
        }

        public AuthorizedUser MakeInvalidToken()
        {
            return Decode("");
        }

        private static SecurityTokenDescriptor MakeDescriptor(User user, DateTime? dateTime = null)
        {
            return new()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = GetExpirationDate(dateTime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
        }

        private static DateTime GetExpirationDate(DateTime? dateTime)
        {
            return dateTime ?? DateTime.UtcNow.AddDays(2);
        }
    }

    public class AuthorizedUser
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

        private Claim GetClaim(string type)
        {
            EnsureTokenIsStillValid();
            return _token.Claims.FirstOrDefault(e => e.Type == type)
                   ?? new Claim("unknown", "");
        }

        private void EnsureTokenIsStillValid()
        {
            if (IsExpired)
            {
                throw new UnauthorizedException("Unable to access resource, token expired.");
            }
        }

        public override string ToString()
        {
            return new JwtSecurityTokenHandler().WriteToken(_token);
        }

        public async Task EnsureCanAccessOrganization(string organizationId)
        {
            var organizations = await _gateway.GetOrganizations(UserId);
            if (organizations.All(e => e.Name != organizationId))
            {
                throw new UnauthorizedException($"[{UserId}] doesn't have access to [{organizationId}]");
            }
        }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}
