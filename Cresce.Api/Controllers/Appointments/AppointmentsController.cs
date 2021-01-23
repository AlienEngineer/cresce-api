using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cresce.Api.Models;
using Cresce.Core.Appointments;
using Cresce.Core.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Cresce.Api.Controllers.Appointments
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentServices _service;

        public AppointmentsController(IAppointmentServices service) => _service = service;

        [HttpGet]
        public async Task<IEnumerable<AppointmentModel>> GetAppointments(
            [FromHeader] IEmployeeAuthorization authorization)
        {
            return (await _service.GetAppointments(authorization))
                .Select(entity => new AppointmentModel(entity));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(
            [FromHeader] IEmployeeAuthorization authorization,
            NewAppointmentModel appointment)
        {
            var newAppointment = await _service.CreateAppointment(appointment.Unwrap(), authorization);
            return Created(
                $"api/v1/appointment/{newAppointment.Id}",
                new AppointmentModel(
                    newAppointment
                )
            );
        }
    }
}
