using System;
using System.Threading.Tasks;
using Cresce.Core.Appointments;
using Cresce.Core.Authentication;
using NUnit.Framework;

namespace Cresce.Core.Tests.Appointments
{
    public class GetAppointmentsTests : ServicesTests<IAppointmentServices>
    {
        [Test]
        public async Task Get_appointments_lists_returns_the_full_list_of_services()
        {
            var services = MakeService();

            var entities = await services.GetAppointments(GetEmployeeAuthorization());

            CollectionAssert.AreEqual(new []
            {
                new Appointment
                {
                    Id = 1,
                    Discount = 10.0,
                    Hours = 3.5,
                    Value = 30.0,
                    CustomerId = 1,
                    EmployeeId = 1,
                    ServiceId = 1,
                    StartedAt = new DateTime(2020, 02, 10)
                },
            }, entities);
        }

        [Test]
        public void Getting_appointments_with_expired_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.GetAppointments(GetExpiredEmployeeAuthorization())
            );
        }

        [Test]
        public void Getting_appointments_with_invalid_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.GetAppointments(GetInvalidEmployeeAuthorization())
            );
        }
    }
}
