using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Appointments;
using Cresce.Core.Authentication;
using NUnit.Framework;

namespace Cresce.Core.Tests.Appointments
{
    public class CreateAppointmentTests : ServicesTests<IAppointmentServices>
    {
        [Test]
        public async Task Creating_an_appointment_stores_the_given_appointment()
        {
            var services = MakeService();
            var appointment = new Appointment
            {
                Hours = 2.5,
                CustomerId = 1,
                EmployeeId = 1,
                ServiceId = 1,
                StartedAt = new DateTime(2020, 02, 12)
            };

            await services.CreateAppointment(appointment, GetEmployeeAuthorization());

            await AssertAppointmentIsStored(services, appointment with {Id = 2});
        }

        [Test]
        public async Task Creating_an_appointment_return_newly_created_appointment()
        {
            var services = MakeService();
            var appointment = new Appointment
            {
                Hours = 2.5,
                CustomerId = 1,
                EmployeeId = 1,
                ServiceId = 1,
                StartedAt = new DateTime(2020, 02, 12)
            };

            var createdAppointment = await services.CreateAppointment(appointment, GetEmployeeAuthorization());

            Assert.That(createdAppointment, Is.EqualTo(appointment with {Id = 2}));
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

        private async Task AssertAppointmentIsStored(IAppointmentServices services, Appointment newAppointment)
        {
            CollectionAssert.Contains(
                await services.GetAppointments(GetEmployeeAuthorization()),
                newAppointment
            );
        }
    }
}
