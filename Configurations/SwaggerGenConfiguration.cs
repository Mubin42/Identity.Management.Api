using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Identity.Management.Api.Configurations;

public static class SwaggerGenConfiguration
{

  /// <summary>
  /// Configures swagger generation for the application.
  /// </summary>
  public static IServiceCollection SwaggerGenConfigure(this IServiceCollection services)
  {
    services.AddSwaggerGen(options =>
    {
      // Add swagger doc version and title
      options.SwaggerDoc("v1", new OpenApiInfo
      {
        Version = "v1",
        Title = "Zolo Backend",
      });

      // Add security definition for JWT
      options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
      });

      options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

    return services;
  }
}

