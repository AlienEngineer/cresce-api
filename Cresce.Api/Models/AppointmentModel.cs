using System;
using Cresce.Core;
using Cresce.Core.Appointments;

namespace Cresce.Api.Models
{
    public record AppointmentModel : IUnwrap<Appointment>
    {
        public AppointmentModel(Appointment entity)
        {
            Id = entity.Id;
            StartedAt = entity.StartedAt;
            ServiceId = entity.ServiceId;
            EmployeeId = entity.EmployeeId;
            CustomerId = entity.CustomerId;
            Hours = entity.Hours;
            Discount = entity.Discount;
            Value = entity.Value;
        }

        public Appointment Unwrap()
        {
            return new()
            {
                Id = Id,
                StartedAt = StartedAt,
                ServiceId = ServiceId,
                EmployeeId = EmployeeId,
                CustomerId = CustomerId,
                Hours = Hours,
                Discount = Discount,
                Value = Value,
            };
        }

        public AppointmentModel()
        {
        }

        public int Id { get; init; }

        public DateTime StartedAt { get; init; }

        public int ServiceId { get; init; }

        public int EmployeeId { get; init; }

        public int CustomerId { get; init; }

        public double Hours { get; init; }

        public double Discount { get; init; }

        public double Value { get; set; }
    }
}