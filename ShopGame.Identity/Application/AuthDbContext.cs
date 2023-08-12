using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopGame.Identity.ConnectionConfiguration;
using ShopGame.Identity.EntityConfiguration;
using ShopGame.Identity.Models;

namespace ShopGame.Identity.Application;

public class AuthDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,int>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        
    }
    
    public AuthDbContext()
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {

            optionsBuilder.UseMySql(ConnectionString.ConnectString(),
                    ServerVersion.AutoDetect(ConnectionString.ConnectString()))
                .LogTo(s => System.Diagnostics.Debug.WriteLine(s),LogLevel.Trace);


        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        if (Database.IsRelational())
        {

            modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());

        }
        
        
       
    }
}