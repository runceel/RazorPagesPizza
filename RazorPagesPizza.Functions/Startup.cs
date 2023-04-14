using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using RazorPagesPizza.Functions;

[assembly: FunctionsStartup(typeof(Startup))]

namespace RazorPagesPizza.Functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services
            .AddApplicationServices(builder.GetContext().Configuration)
            .AddRepositories(builder.GetContext().Configuration);
    }
}
