using System;

namespace Cresce.Core.Appointments
{
    public record Appointment
    {
        public int Id { get; init; }
        public DateTime StartedAt { get; init; }
        public int ServiceId { get; init; }
        public int EmployeeId { get; init; }
        public int CustomerId { get; init; }
        public double Hours { get; init; }
        public double Discount { get; init; }
        public double Value { get; init; }
    }
}
