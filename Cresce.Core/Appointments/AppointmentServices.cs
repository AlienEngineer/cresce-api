using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.GetEntities;

namespace Cresce.Core.Appointments
{
    internal class AppointmentServices : IAppointmentServices
    {
        private readonly IGetEntitiesService<Appointment> _getEntitiesGateway;
        private readonly ICreateEntityGateway<Appointment> _createEntityGateway;

        public AppointmentServices(
            IGetEntitiesService<Appointment> getEntitiesGateway,
            ICreateEntityGateway<Appointment> createEntityGateway
        )
        {
            _getEntitiesGateway = getEntitiesGateway;
            _createEntityGateway = createEntityGateway;
        }

        public Task<IEnumerable<Appointment>> GetAppointments(IEmployeeAuthorization authorization)
            => _getEntitiesGateway.GetEntities(authorization);

        public async Task<Appointment> CreateAppointment(Appointment appointment, IEmployeeAuthorization authorization)
        {
            authorization.EnsureIsValid();

            return appointment with
            {
                Id = await _createEntityGateway.Create(appointment)
            };
        }
    }
}
