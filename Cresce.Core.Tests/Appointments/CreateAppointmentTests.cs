using System;
using System.Threading.Tasks;
using Cresce.Core.Appointments;
using Cresce.Core.Authentication;
using NUnit.Framework;

namespace Cresce.Core.Tests.Appointments
{
    public class CreateAppointmentTests : ServicesTests<IAppointmentServices>
    {
        [Test]
        public async Task Creating_an_appointments_stores_the_given_appointment()
        {
            var services = MakeService();

            await services.CreateAppointment(new Appointment
            {
                Hours = 2.5,
                CustomerId = 1,
                EmployeeId = 1,
                ServiceId = 1,
                StartedAt = new DateTime(2020, 02, 12)
            }, GetEmployeeAuthorization());

            var entities = await services.GetAppointments(GetEmployeeAuthorization());
            CollectionAssert.Contains(
                entities,
                new Appointment
                {
                    Id = 2,
                    Hours = 2.5,
                    CustomerId = 1,
                    EmployeeId = 1,
                    ServiceId = 1,
                    StartedAt = new DateTime(2020, 02, 12)
                }
            );
        }

        [Test]
        public void Getting_appointments_with_expired_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.CreateAppointment(new Appointment(), GetExpiredEmployeeAuthorization())
            );
        }

        [Test]
        public void Getting_appointments_with_invalid_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.CreateAppointment(new Appointment(), GetInvalidEmployeeAuthorization())
            );
        }
    }
}