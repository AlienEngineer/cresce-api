using System.Net;
using System.Threading.Tasks;
using Cresce.Api.Models;
using NUnit.Framework;

namespace Cresce.Api.Tests.Controllers.EmployeeScope
{
    [TestFixture(typeof(CustomerRequests), typeof(CustomerModel))]
    [TestFixture(typeof(ServiceRequests), typeof(ServiceModel))]
    [TestFixture(typeof(AppointmentsRequests), typeof(AppointmentModel))]
    public class GetEntitiesTests<T, TEntity> : WebApiTests
        where T : ControllerRequests<TEntity>, new()
    {
        private readonly ControllerRequests<TEntity> _requests;

        public GetEntitiesTests()
        {
            _requests = new T
            {
                Context = this
            };
        }

        [Test]
        public async Task Getting_entities_returns_the_list_of_entities()
        {
            var client = await GetAuthenticatedEmployeeClient();

            var response = await client.GetAsync(_requests.EntitiesUrl);

            await ResponseAssert.ListAreEquals(_requests.GetExpectedList(), response);
        }

        [Test]
        public async Task Getting_entities_without_token_returns_401()
        {
            var client = GetClient();

            var response = await client.GetAsync(_requests.EntitiesUrl);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Getting_entities_with_expired_token_returns_401()
        {
            var client = GetExpiredClient();

            var response = await client.GetAsync(_requests.EntitiesUrl);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Getting_entities_without_employee_token_returns_401()
        {
            var client = await GetAuthenticatedClient();

            var response = await client.GetAsync(_requests.EntitiesUrl);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }

    public abstract class ControllerRequests<TEntity>
    {
        public abstract string EntitiesUrl { get; }
        public WebApiTests Context { get; set; } = null!;
        public abstract TEntity[] GetExpectedList();
    }
}