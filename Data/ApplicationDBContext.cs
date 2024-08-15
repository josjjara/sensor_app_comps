using Microsoft.EntityFrameworkCore;
using SensorApp.Models;

namespace SensorApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Parameter> Parameters => Set<Parameter>();
    public DbSet<SensorData> SensorData => Set<SensorData>();

} 