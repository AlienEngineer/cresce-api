using System;
using Cresce.Core;
using Cresce.Core.Appointments;

namespace Cresce.Api.Models
{
    public record NewAppointmentModel : IUnwrap<Appointment>
    {
        public Appointment Unwrap()
        {
            return new()
            {
                ServiceId = ServiceId,
                CustomerId = CustomerId,
                StartedAt = StartedAt,
                Hours = Hours
            };
        }

        public DateTime StartedAt { get; init; } = DateTime.Now;

        public int ServiceId { get; init; } = -1;

        public int CustomerId { get; init; } = -1;

        public double Hours { get; init; } = -1;
    }
}