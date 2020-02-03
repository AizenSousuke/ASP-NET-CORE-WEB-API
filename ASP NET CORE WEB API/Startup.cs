using ASP_NET_CORE_WEB_API.DbContexts;
using ASP_NET_CORE_WEB_API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ASP_NET_CORE_WEB_API
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
            services.AddControllers(setupAction => 
            {
                // To return 406 not acceptable errors if client send !application/json
                setupAction.ReturnHttpNotAcceptable = true;
                // Add support for other types using output formatters
            }).AddXmlDataContractSerializerFormatters();
             
            services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();

            services.AddDbContext<CourseLibraryContext>(options =>
            {
                 var connection_local = @"Server=(localdb)\MSSQLLocalDB;Database=CourseLibraryDB;Trusted_Connection=True;";
                
                // Docker - sql,1433 is referring to sql server in docker-compose file
                // var connection_sql = @"Server=sql,1433\MSSQLLocalDB;Database=CourseLibraryDB;uid=sa;password=P@ssw0rd;";

                options.UseSqlServer(connection_local);
            });

            // Added automapper nuget package to map object to object in Author.cs
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                // If not in development environment
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                        // Log error here (outside scope of the course)
                    });
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
