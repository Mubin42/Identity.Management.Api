using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Management.Api.Data
{
  public class AppDbContext : IdentityDbContext<IdentityUser>
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    // Add the models to the context here


    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      SeedRoles(builder);

    }

    private static void SeedRoles(ModelBuilder builder)
    {
      // Seed roles here
      builder.Entity<IdentityRole>().HasData(
          new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
          new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" },
          new IdentityRole { Id = "3", Name = "Customer", NormalizedName = "CUSTOMER" }
      );
    }

  }
}




