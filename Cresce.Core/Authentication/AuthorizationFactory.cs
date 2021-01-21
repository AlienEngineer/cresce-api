using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cresce.Core.Organizations;
using Cresce.Core.Users;
using Microsoft.IdentityModel.Tokens;

namespace Cresce.Core.Authentication
{
    internal class AuthorizationFactory : IAuthorizationFactory
    {
        private readonly IGetUserOrganizationsGateway _gateway;
        private readonly Settings _settings;

        public AuthorizationFactory(IGetUserOrganizationsGateway gateway, Settings settings)
        {
            _gateway = gateway;
            _settings = settings;
        }

        public IAuthorization DecodeAuthorization(string token) =>
            new UserAuthorization(MakeJwtSecurityToken(token), _gateway);

        public IEmployeeAuthorization DecodeEmployeeAuthorization(string token) =>
            new AuthorizedEmployee(MakeJwtSecurityToken(token), _gateway);

        private static JwtSecurityToken MakeJwtSecurityToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.CanReadToken(token)
                ? tokenHandler.ReadJwtToken(token)
                : new JwtSecurityToken();
        }

        public IAuthorization MakeAuthorization(User user, DateTime? dateTime = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(MakeDescriptor(user, dateTime));
            return new UserAuthorization((JwtSecurityToken) token, _gateway);
        }

        public IEmployeeAuthorization GetAuthorizedEmployee(IAuthorization user, string employeeId)
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

        public IEmployeeAuthorization MakeExpiredEmployeeAuthorization() =>
            new AuthorizedEmployee(new JwtSecurityToken(), _gateway);

        public IAuthorization MakeExpiredAuthorization() => new UserAuthorization(new JwtSecurityToken(), _gateway);

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
