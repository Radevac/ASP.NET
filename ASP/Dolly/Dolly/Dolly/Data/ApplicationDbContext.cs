using Dolly.Models.Dolly;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dolly.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<DollModel> Dolls { get; set; }
    public DbSet<OutfitModel> Outfits { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}