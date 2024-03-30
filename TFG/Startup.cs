using InstagramApiSharp.API;
using InstagramApiSharp.Classes.Models;
using Microsoft.EntityFrameworkCore;
using TFG.Models;
using TFG.Services;
using TFG.Services.Interfaces;

namespace TFG
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
            },
            typeof(ModelMappingProfile));

            services.AddScoped<IInstagramApiService, InstagramApiService>();
            services.AddScoped<IInstagramMediaService, InstagramMediaService>();
            services.AddScoped<IInstagramLogService, InstagramLogService>();

            services.AddControllersWithViews();

            // Replace with your connection string.
            var connectionString = $"server=localhost;user=root;password=root;database=TFG";

            // Replace with your server version and type.
            // Use 'MariaDbServerVersion' for MariaDB.
            // Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
            // For common usages, see pull request #1233.
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));

            //Replace 'YourDbContext' with the name of your own DbContext derived class.
            services.AddDbContext<TFGContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    // The following three options help with debugging, but should
                    // be changed or removed for production.
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
