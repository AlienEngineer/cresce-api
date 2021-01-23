using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cresce.Api.Models;
using NUnit.Framework;

namespace Cresce.Api.Tests.Controllers.EmployeeScope
{
    public class AppointmentsControllerTests : WebApiTests
    {
        [Test]
        public async Task Posting_an_appointment_returns_201()
        {
            var client = await GetAuthenticatedEmployeeClient();

            var response =  await client.PostAsJsonAsync(
                "api/v1/appointments",
                new NewAppointmentModel
                {
                    Hours = 4.0,
                    CustomerId = 1,
                    ServiceId = 1,
                    StartedAt = new DateTime(2020, 01, 23)
                }
            );

            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(await response.GetContent<AppointmentModel>(), Is.EqualTo(new AppointmentModel
            {
                Hours = 4.0,
                CustomerId = 1,
                ServiceId = 1,
                StartedAt = new DateTime(2020, 01, 23)
            }));
        }
    }
}
