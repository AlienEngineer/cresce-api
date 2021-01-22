using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.GetEntities;

namespace Cresce.Core.Appointments
{
    internal class AppointmentServices : IAppointmentServices
    {
        private readonly IGetEntitiesService<Appointment> _getEntitiesGateway;

        public AppointmentServices(IGetEntitiesService<Appointment> getEntitiesGateway)
        {
            _getEntitiesGateway = getEntitiesGateway;
        }

        public Task<IEnumerable<Appointment>> GetAppointments(IEmployeeAuthorization authorization)
            => _getEntitiesGateway.GetEntities(authorization);
    }
}
