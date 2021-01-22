using Cresce.Core.Appointments;

namespace Cresce.Core.Sql.Appointments
{
    internal class AppointmentModel : IUnwrap<Appointment>
    {
        public int Id { get; set; }

        public Appointment Unwrap()
        {
            return new Appointment
            {
                Id = Id,
            };
        }
    }
}
