using Microsoft.OpenApi.Models;
using System.Reflection;

namespace AlicundeTest.API.Infraestructure;

public static class SwaggerConfiguration
{

    /// <summary>
    /// Swagger custom configuration
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AlicundeTest API",
                Description = "Alicunde coding test",
                Version = "v1",
            });

            // Documentation configuration
            string? xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string? xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        return services;
    }

    /// <summary>
    /// Adding swagger middleware
    /// </summary>
    /// <param name="app"></param>
    public static WebApplication UseCustomSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "AlicundeTest API V1");
            c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
        });

        return app;
    }
}
