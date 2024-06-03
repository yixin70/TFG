using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TFG.Models;
using TFG.Services;
using TFG.Services.Interfaces;
using WorkerRefresh;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<IInstagramApiService, InstagramApiService>();
builder.Services.AddScoped<IInstagramLogService, InstagramLogService>();
builder.Services.AddScoped<ITwitterLogService, TwitterLogService>();

builder.Services.AddAutoMapper(cfg =>
{
},typeof(ModelMappingProfile));


// Replace with your connection string.
var connectionString = $"server=localhost;user=root;password=root;database=TFG";

// Replace with your server version and type.
// Use 'MariaDbServerVersion' for MariaDB.
// Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
// For common usages, see pull request #1233.
var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));

//Replace 'YourDbContext' with the name of your own DbContext derived class.
builder.Services.AddDbContext<TFGContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)
        // The following three options help with debugging, but should
        // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        //.EnableSensitiveDataLogging()
        //.EnableDetailedErrors()
);


var host = builder.Build();
host.Run();
