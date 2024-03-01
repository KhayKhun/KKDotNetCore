using KKDotNetCore.MinimalApi;
using KKDotNetCore.MinimalApi.Feature.User;
using KKDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/myapp.log", rollingInterval: RollingInterval.Hour)
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));

    }, ServiceLifetime.Transient, ServiceLifetime.Transient);

    builder.Services.AddScoped(n =>
        new AdoDotNetService(
            new SqlConnectionStringBuilder(
                builder.Configuration.GetConnectionString("DbConnection")
            )
        )
    );
    builder.Services.AddScoped(n =>
        new DapperService(
            new SqlConnectionStringBuilder(
                builder.Configuration.GetConnectionString("DbConnection")
            )
        )
    );

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // extention method

    //app.UseUserAdoDotNetService();
    //app.UseUserService();
    app.UseUserDapperService();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
