using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entities;

namespace WebApplication1.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<TagModel> Tags { get; set; }
    
    public DbSet<CategoryModel> Categories { get; set; }
    
    public DbSet<PostModel> Posts { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
       
    }
}