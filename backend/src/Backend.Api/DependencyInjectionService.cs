using Microsoft.OpenApi.Models;
using System.Reflection;
using Serilog;

namespace Backend.Api
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "People Paynau API",
                    Description="Administración de APIs para Paynau Test"
                });

                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));

            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontendLocalhost",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000", "http://localhost:5000", "http://localhost:3001")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });


            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug() 
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();


            return services;
        }
    }
}
