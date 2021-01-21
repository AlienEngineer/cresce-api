using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Customers;
using NUnit.Framework;

namespace Cresce.Core.Tests.Customers
{
    public class GetCustomersTests : ServicesTests<ICustomerServices>
    {
        [Test]
        public async Task Get_customers_lists_returns_the_full_list_of_services()
        {
            var services = MakeService();

            var employees = await services.GetCustomers(GetEmployeeAuthorization());

            CollectionAssert.AreEqual(new []
            {
                new Customer
                {
                    Id = 1,
                    Name = "Diogo Quintas",
                    Image = GetSampleImage(),
                },
            }, employees);
        }

        [Test]
        public void Getting_customers_with_expired_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.GetCustomers(GetExpiredEmployeeAuthorization())
            );
        }

        [Test]
        public void Getting_customers_with_authentication_without_employee_id_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.GetCustomers(GetInvalidEmployeeAuthorization())
            );
        }
    }
}
