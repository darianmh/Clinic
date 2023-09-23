using Appointment.Contract.Repositories;
using Appointment.Contract.Services;
using Appointment.Contract.UseCases;
using Appointment.Data;
using Appointment.Frameworks.EfCore;
using Appointment.Services;
using Appointment.UseCases;

namespace Appointment.Grpc.Extensions
{
    public static class ServiceCollectionHelper
    {
        public static IServiceCollection AddAppointmentServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddEntityFrameworkSqlServer()
                .AddDbContext<AppointmentContext>(options =>
                {
                    options.UseSqlServer(configuration["connectionString"],
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });
                });



            serviceCollection.AddScoped<IAppointmentRepository, AppointmentRepository>();
            serviceCollection.AddScoped<IDoctorRepository, DoctorRepository>();



            serviceCollection.AddScoped<IAppointmentService, AppointmentService>();
            serviceCollection.AddScoped<IDoctorService, DoctorService>();
            serviceCollection.AddScoped<IAppointmentManager, AppointmentManager>();



            return serviceCollection;
        }
    }
}
