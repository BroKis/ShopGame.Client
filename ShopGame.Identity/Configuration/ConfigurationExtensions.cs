using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopGame.Identity.Application;
using ShopGame.Identity.ConnectionConfiguration;
using ShopGame.Identity.Models;
using ShopGame.Identity.Service;

namespace ShopGame.Identity.Configuration;

public static class ConfigurationExtensions
{
    public static void ConfigureIdentity(this IServiceCollection services)
    {

        services.AddDbContext<AuthDbContext>(option
            =>
        {
            option.UseMySql("server=localhost;user=root;password=qf13iqfaenq35_1;database=shopgame;", new MySqlServerVersion("8.0.32"));
        }).AddIdentity<ApplicationUser,ApplicationRole>(opt =>
        {
            opt.SignIn.RequireConfirmedAccount = false;
            opt.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<AuthDbContext>();


        services.AddScoped<IAccountService, AccountService>();



    }
}