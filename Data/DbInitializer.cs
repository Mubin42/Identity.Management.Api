using System;
using Microsoft.EntityFrameworkCore;

namespace Identity.Management.Api.Data;

public class DbInitializer
{
  public static void InitDb(WebApplication app)
  {
    /**
    * Create scope for app services so that it will get 
    * disposed off after finished usage within this scope
    **/
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetService<AppDbContext>() ?? throw new Exception("Could not get the context");

    // See data using the Db Context
    SeedData(context);
  }

  public static void SeedData(AppDbContext context)
  {
    // See implementation: https://github.com/Muhtasim-Fuad-Showmik/LeaseLifestyles/blob/development/src/RentService/Data/DbInitializer.cs

    // Apply any migrations not yet applied to the database
    context.Database.Migrate();

    // Seed data here

    // Update database to add rents from the memory to the database
    context.SaveChanges();
  }
}
