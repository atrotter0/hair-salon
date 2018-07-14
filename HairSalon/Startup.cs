using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HairSalon
{
    public static class DBConfiguration
    {
        public static string ConnectionString = "User ID=" + System.Environment.GetEnvironmentVariable("DATABASE_USER") +
                                                ";Password=" + System.Environment.GetEnvironmentVariable("DATABASE_PASSWORD") +
                                                ";Host=" + System.Environment.GetEnvironmentVariable("DATABASE_HOST") +
                                                ";Port=" + System.Environment.GetEnvironmentVariable("DATABASE_PORT") +
                                                ";Database=" + System.Environment.GetEnvironmentVariable("DATABASE_NAME") +
                                                ";Pooling=true;Use SSL Stream=True;SSL Mode=Require;TrustServerCertificate=True;";
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Invalid path or query. Please try again.");
            });
        }
    }
}
