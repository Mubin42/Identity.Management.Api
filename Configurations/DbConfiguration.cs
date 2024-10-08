using Identity.Management.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Identity.Management.Api.Configurations;

public static class DbConfiguration
{
  public static IServiceCollection AddAppDatabase(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<AppDbContext>(option =>
    {
      // Connection string
      var connectionStr = configuration.GetConnectionString("DefaultConnection");
      option.UseNpgsql(connectionStr);
    });

    return services;
  }
}
