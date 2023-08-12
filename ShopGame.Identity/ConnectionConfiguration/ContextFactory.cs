using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ShopGame.Identity.Application;

namespace ShopGame.Identity.ConnectionConfiguration;

public class ContextFactory:IDesignTimeDbContextFactory<AuthDbContext>
{
    public AuthDbContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder();
        // установка пути к текущему каталогу
        builder.SetBasePath(Directory.GetCurrentDirectory());
        // получаем конфигурацию из файла appsettings.json
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();
        // получаем строку подключения
        string connectionString
            = config.GetConnectionString("DefaultConnection");
        var optionsBuilder =
            new DbContextOptionsBuilder<AuthDbContext>();
        var seviceVersion = new MySqlServerVersion(new Version(8, 0, 32));
        DbContextOptions<AuthDbContext> options
            = optionsBuilder
                .UseMySql(connectionString,seviceVersion).Options;
        return new AuthDbContext(optionsBuilder.Options);


    }
}