using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Services;
using NUnit.Framework;

namespace Cresce.Core.Tests.Services
{
    public class GetServicesTests : ServicesTests<IServiceServices>
    {
        [Test]
        public async Task Get_services_lists_returns_the_full_list_of_services()
        {
            var services = MakeService();

            var employees = await services.GetServices(GetEmployeeAuthorization());

            CollectionAssert.AreEqual(new []
            {
                new Service
                {
                    Id = 1,
                    Name = "Development",
                    Image = GetSampleImage(),
                    Value = 30.0,
                },
            }, employees);
        }

        [Test]
        public void Getting_services_with_expired_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.GetServices(GetExpiredEmployeeAuthorization())
            );
        }

        [Test]
        public void Getting_services_with_authentication_without_employee_id_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.GetServices(GetInvalidEmployeeAuthorization())
            );
        }
    }
}
