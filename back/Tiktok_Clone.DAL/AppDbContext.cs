using Microsoft.EntityFrameworkCore;

namespace Tiktok_Clone.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options)
        :base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
    
    
}