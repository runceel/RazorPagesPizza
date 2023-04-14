using Azure.Core;
using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesPizza.Core.Services;
<<<<<<< HEAD
using RazorPagesPizza.Domain.Repositories;
using RazorPagesPizza.Domain.Services;
using RazorPagesPizza.Implements.Repositories;
using RazorPagesPizza.Repositories;
using RazorPagesPizza.Repositories.Options;
=======
using RazorPagesPizza.Implements.Repositories;
using RazorPagesPizza.Repositories;
using RazorPagesPizza.Repositories.Options;
using RazorPagesPizza.Services;
>>>>>>> 04eff41e986823ee2e3c579ae5da74ac2cce44d6

namespace RazorPagesPizza.Functions;

public static class ServiceRegister
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration _)
    {
        services.AddSingleton<IManagePizzaInventoryService, ManagePizzaInventoryService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        // options
        services.Configure<OpenAIOptions>(configuration.GetSection(nameof(OpenAIOptions)));

        // repositories
        services.AddSingleton<IPizzaRepository, PizzaRepository>();
        services.AddSingleton<IPizzaDescriptionGenerator, PizzaDescriptionGenerator>();

        // services
        services.AddSingleton<IPizzaDescriptionGenerateService, PizzaDescriptionGenerateService>();

        // Azure
        services.AddAzureClients(clientBuilder =>
        {
            // Cosmos DB
            clientBuilder.AddClient((CosmosOptions options, TokenCredential credential) =>
            {
                return new CosmosClient(options.AccountEndpoint, credential, new CosmosClientOptions
                {
                    SerializerOptions = new()
                    {
                        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
                    },
                });
            }).ConfigureOptions(configuration.GetSection(nameof(CosmosOptions)));

            // OpenAI
            // TODO: 追加する

            // credential
            clientBuilder.UseCredential(new DefaultAzureCredential());
        });

        return services;
    }

}
