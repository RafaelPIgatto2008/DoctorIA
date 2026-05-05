using DoctorIA.Services;

namespace DoctorIA;

public class DependencyInjection()
{
    public static IServiceCollection AddInfraestructure(IServiceCollection services)
    {
            services.AddHttpClient<IServicesIa, ServiceIA>();
            
            return services;
    }
}