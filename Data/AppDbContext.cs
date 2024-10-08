using Microsoft.EntityFrameworkCore;

namespace Identity.Management.Api.Data
{
  public class AppDbContext(DbContextOptions option) : DbContext(option)
  {
    // Add the models to the context here

  }
}




