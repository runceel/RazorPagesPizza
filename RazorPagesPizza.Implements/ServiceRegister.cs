using Azure.Core;
using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesPizza.Core.Services;
using RazorPagesPizza.Implements;
using RazorPagesPizza.Implements.Repositories;
using RazorPagesPizza.Repositories.Options;
using RazorPagesPizza.Services;

namespace RazorPagesPizza.Repositories;

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

        // services
        services.AddSingleton<IPizzaRepository, PizzaRepository>();
        services.AddSingleton<IPizzaDescriptionGenerateRequester, PizzaDescriptionGenerateRequester>();

        // Azure
        services.AddAzureClients(clientBuilder =>
        {
            // Queue
            var queueOptions = configuration.GetRequiredSection(nameof(QueueOptions)).Get<QueueOptions>();
            clientBuilder.AddQueueServiceClient(new Uri(queueOptions.StorageUrl));

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
            var openAiOptions = configuration.GetSection(nameof(OpenAIOptions)).Get<OpenAIOptions>();
            clientBuilder.AddOpenAIClient(new(openAiOptions.Endpoint), new(openAiOptions.Key));

            // credential
            clientBuilder.UseCredential(new DefaultAzureCredential());
        });

        return services;
    }

}
