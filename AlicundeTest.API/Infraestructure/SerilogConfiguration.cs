using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Console;
using Serilog;

namespace AlicundeTest.API.Infraestructure;

public static class SerilogConfiguration
{
    /// <summary>
    /// Serilog custom configuration
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services));

        return builder;
    }
}
