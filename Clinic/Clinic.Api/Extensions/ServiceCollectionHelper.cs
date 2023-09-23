using Clinic.Api.Infrastructure.Appointment;
using Clinic.Api.Services.Appointment;

namespace Clinic.Api.Extensions
{
    public static class ServiceCollectionHelper
    {
        public static IServiceCollection AddAppointmentServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddGrpcClient<Appointment.Grpc.Protos.AppointmentService.AppointmentServiceClient>(x =>
            {
                x.Address = new Uri(configuration["AppointmentClient"]);
            });
            serviceCollection.AddScoped<IAppointmentService, AppointmentService>();

            return serviceCollection;
        }
    }
}
