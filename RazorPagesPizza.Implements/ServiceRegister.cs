using Azure.Core;
using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesPizza.Implements.Repositories;
using RazorPagesPizza.Services;

namespace RazorPagesPizza.Implements;

public static class ServiceRegister
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration _)
    {
        services.AddSingleton<IPizzaService, PizzaService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPizzaRepository, PizzaRepository>();
        services.AddSingleton<IPizzaDescriptionGenerator, PizzaDescriptionGenerator>();

        services.AddAzureClients(clientBuilder =>
        {
            var queueOptions = configuration.GetSection(nameof(QueueOptions)).Get<QueueOptions>();
            clientBuilder.AddQueueServiceClient(new Uri(queueOptions.StorageUrl));
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

            clientBuilder.UseCredential(new DefaultAzureCredential());
        });

        return services;
    }

}
