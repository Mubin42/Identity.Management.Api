using Identity.Management.Api.Configurations;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add swagger doc version and title
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Identity Management Api",
    });
});

// Configure services
builder.Services.AddAppDatabase(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
