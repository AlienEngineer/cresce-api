using Microsoft.Extensions.DependencyInjection;

namespace Cresce.Core.Appointments
{
    public class AppointmentsModule : IServicesModule
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterGetEntities<Appointment>();
            serviceCollection.AddTransient<IAppointmentServices, AppointmentServices>();
        }
    }
}