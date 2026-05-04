using Microsoft.EntityFrameworkCore;

namespace DoctorIA.Data;

public class DoctorDbContext(DbContextOptions<DoctorDbContext> options) : DbContext
{
    private readonly DbContextOptions<DoctorDbContext> _options = 
        options ?? throw new ArgumentNullException(nameof(options));
}