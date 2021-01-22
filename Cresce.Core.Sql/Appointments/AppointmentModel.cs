using System;
using Cresce.Core.Appointments;

namespace Cresce.Core.Sql.Appointments
{
    internal class AppointmentModel : IUnwrap<Appointment>
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public double Hours { get; set; }
        public double Discount { get; set; }
        public double Value { get; set; }

        public Appointment Unwrap()
        {
            return new Appointment
            {
                Id = Id,
                StartedAt = StartedAt,
                ServiceId = ServiceId,
                EmployeeId = EmployeeId,
                CustomerId = CustomerId,
                Hours = Hours,
                Discount = Discount,
                Value = Value
            };
        }
    }
}
