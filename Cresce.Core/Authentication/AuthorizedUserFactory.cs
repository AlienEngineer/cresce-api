using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cresce.Core.Organizations;
using Cresce.Core.Users;
using Microsoft.IdentityModel.Tokens;

namespace Cresce.Core.Authentication
{
    internal class AuthorizedUserFactory : IAuthorizedUserFactory
    {

        private readonly IGetUserOrganizationsGateway _gateway;
        private readonly Settings _settings;

        public AuthorizedUserFactory(IGetUserOrganizationsGateway gateway, Settings settings)
        {
            _gateway = gateway;
            _settings = settings;
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

        public AuthorizedUser GetAuthorizedUser(User user, DateTime? dateTime = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(MakeDescriptor(user, dateTime));
            return new AuthorizedUser((JwtSecurityToken) token, _gateway);
        }

        public AuthorizedEmployee GetAuthorizedEmployee(AuthorizedUser user, string employeeId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(
                MakeDescriptor(
                    user.ToUser(),
                    user.ExpirationDate,
                    employeeId
                )
            );
            return new AuthorizedEmployee((JwtSecurityToken) token, _gateway);
        }

        public AuthorizedUser MakeInvalidToken() => Decode("");

        private SecurityTokenDescriptor MakeDescriptor(
            User user,
            DateTime? dateTime = null,
            string? employeeId = null
        )
        {
            return new()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.UserData, employeeId ?? "")
                }),
                Expires = GetExpirationDate(dateTime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.AppKey)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
        }

        private static DateTime GetExpirationDate(DateTime? dateTime) => dateTime ?? DateTime.UtcNow.AddDays(2);
    }


}
