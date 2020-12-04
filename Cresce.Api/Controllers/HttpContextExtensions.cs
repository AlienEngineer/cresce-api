using System.Linq;
using Cresce.Core.Authentication;
using Microsoft.AspNetCore.Http;

namespace Cresce.Api.Controllers
{
    public static class HttpContextExtensions
    {
        public static AuthorizedUser GetUser(this HttpRequest request, ITokenFactory tokenFactory)
        {
            var token = request.Headers["Authorization"][0].Split(" ").Skip(1).First();
            return tokenFactory.Decode(token);
        }
    }
}
