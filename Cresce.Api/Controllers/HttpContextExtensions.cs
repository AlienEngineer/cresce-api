using System.Linq;
using System.Net.Http;
using Cresce.Core.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Cresce.Api.Controllers
{
    public static class HttpContextExtensions
    {
        public static IAuthorization GetAuthorization(
            this HttpRequest request,
            IAuthorizationFactory authorizationFactory
        )
        {
            if (HasInvalidHeader(request))
            {
                throw new HttpRequestException();
            }

            var token = ExtractToken(request);

            return authorizationFactory.DecodeAuthorization(token);
        }

        public static IEmployeeAuthorization GetEmployeeAuthorization(
            this HttpRequest request,
            IAuthorizationFactory authorizationFactory
        )
        {
            if (HasInvalidHeader(request))
            {
                throw new HttpRequestException();
            }

            var token = ExtractToken(request);

            return authorizationFactory.DecodeEmployeeAuthorization(token);
        }

        private static string ExtractToken(HttpRequest request) =>
            GetAuthorizationHeader(request)[0].Split(" ").Skip(1).First();

        private static bool HasInvalidHeader(HttpRequest request)
        {
            var authorizationHeader = GetAuthorizationHeader(request);
            return !authorizationHeader.Any() ||
                   !HasBearer(authorizationHeader);
        }

        private static bool HasBearer(StringValues authorizationHeader)
        {
            var tokenParts = GetTokenParts(authorizationHeader);
            return tokenParts.Length == 2 && tokenParts[0].ToLower() == "bearer";
        }

        private static string[] GetTokenParts(StringValues authorizationHeader) => authorizationHeader[0].Split(" ");

        private static StringValues GetAuthorizationHeader(HttpRequest request) => request.Headers["Authorization"];
    }
}
