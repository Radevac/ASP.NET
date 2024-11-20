using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationGeo.Models.Cars.Toyota;

namespace Toyota_Pagination.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ToyotaModel> ToyotaModels { get; set; }
    
    public DbSet<ConfigurationModel> Configurations { get; set; }
    
    public DbSet<ConfigurationColorsModel> ConfigurationColors { get; set; }
    
    
    public DbSet<ColorModel> Colors { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}