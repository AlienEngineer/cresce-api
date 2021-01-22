using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Appointments
{
    public interface IAppointmentServices
    {
        Task<IEnumerable<Appointment>> GetAppointments(IEmployeeAuthorization authorization);
    }
}
