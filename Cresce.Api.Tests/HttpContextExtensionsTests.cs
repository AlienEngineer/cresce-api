using System.Net.Http;
using Cresce.Api.Controllers;
using Cresce.Core.Authentication;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace Cresce.Api.Tests
{

    public class HttpContextExtensionsTests : WebApiTests
    {
        [TestCase("bearer token")]
        [TestCase("Bearer token")]
        public void Getting_user_from_request_returns_user_with_authorization_header(string token)
        {
            var request = MakeHttpRequest(token);

            var user = request.GetUser(MakeTokenFactory());

            Assert.That(user, Is.Not.Null);
        }

        [Test]
        public void Getting_user_from_request_without_authorization_header_throws()
        {
            var request = MakeHttpRequest(null);

            Assert.Catch<HttpRequestException>(() => request.GetUser(MakeTokenFactory()));
        }

        [TestCase("null")]
        [TestCase("basic X")]
        public void Getting_user_from_request_with_malformed_authorization_header_throws(string token)
        {
            var request = MakeHttpRequest(token);

            Assert.Catch<HttpRequestException>(() => request.GetUser(MakeTokenFactory()));
        }

        private static HttpRequest MakeHttpRequest(string bearerToken)
        {
            var mock = new Mock<HttpRequest>();
            mock.Setup(e => e.Headers)
                .Returns(new HeaderDictionary
                {
                    new("Authorization", bearerToken)
                });

            return mock.Object;
        }
    }
}
