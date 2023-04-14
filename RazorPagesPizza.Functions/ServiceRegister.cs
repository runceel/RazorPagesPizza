﻿using Azure.Core;
using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesPizza.Core.Services;
using RazorPagesPizza.Implements.Repositories;
using RazorPagesPizza.Repositories;
using RazorPagesPizza.Repositories.Options;
using RazorPagesPizza.Services;

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