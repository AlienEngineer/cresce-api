using System.Threading.Tasks;
using Cresce.Core.Employees.GetEmployees;
using Cresce.Core.Services;
using Cresce.Core.Services.GetServices;
using NUnit.Framework;

namespace Cresce.Core.Tests.Services
{
    public class GetServicesTests : ServicesTests<ServiceServices>
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
    }
}
