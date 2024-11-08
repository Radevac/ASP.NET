using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationGeo_2.Models.Entities.Geo;

namespace WebApplicationGeo_2.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<CountryModel> Countries { get; set; }
    public DbSet<AreaModel> Areas { get; set; }
    public DbSet<CityModel> Cities { get; set; }
    public DbSet<RegionModel> Regions { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка отношения один ко многим между AreaModel и CityModel
        modelBuilder.Entity<AreaModel>()
            .HasMany(a => a.Cities)  // Связь с коллекцией Cities
            .WithOne(c => c.Area)     // Обратная связь с Area в CityModel
            .HasForeignKey(c => c.AreaId)  // Указание внешнего ключа в CityModel
            .OnDelete(DeleteBehavior.Restrict);  // Выберите поведение при удалении
    }
}