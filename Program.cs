using Identity.Management.Api.Data;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddDbContext<AppDbContext>(option =>
{
    // Connection string
    var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseNpgsql(connectionStr);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
