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

  }
}




