using DoctorIA.Data;
using Microsoft.EntityFrameworkCore;

namespace DoctorIA;

public class DependencyInjection()
{
    public static IServiceCollection AddInfraestructure(IServiceCollection services, 
        IConfiguration configuration)
    {
        // Connection string for DB
        var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DoctorDbContext>(options =>
            options.UseNpgsql(connectionString));
            
        
            return services;
    }
}