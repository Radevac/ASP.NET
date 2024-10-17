using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFirstWebServer.Models.Entities;

namespace MyFirstWebServer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<SubscribeModel> Subscribers { get; set; }
        public DbSet<MyContact> Contacts { get; set; }
        
        public DbSet<PersonModel> Persons { get; set; }
        
        public DbSet<TagModel> Tags { get; set; }
        
        public DbSet<CategoryModel> Categories { get; set; }
        
        public DbSet<VendorModel> Vendors { get; set; }
        
        public DbSet<AreaModel> Areas { get; set; }
    
        public DbSet<CityModel> Cities { get; set; }
    
        public DbSet<CityTypeModel> CityTypes { get; set; }
    
        public DbSet<StreetModel> Streets { get; set; }
    
        public DbSet<HouseModel> Houses { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyFirstWebServer.Models.Entities.AreaModel> AreaModel { get; set; } = default!;
        public DbSet<MyFirstWebServer.Models.Entities.CityModel> CityModel { get; set; } = default!;
        public DbSet<MyFirstWebServer.Models.Entities.CityTypeModel> CityTypeModel { get; set; } = default!;
        public DbSet<MyFirstWebServer.Models.Entities.HouseModel> HouseModel { get; set; } = default!;
        public DbSet<MyFirstWebServer.Models.Entities.StreetModel> StreetModel { get; set; } = default!;
    }
}