using Microsoft.Azure.Cosmos;
using RazorPagesPizza.Core.Services;
using RazorPagesPizza.Domain.Repositories;
using RazorPagesPizza.Models;
using System.Net;

namespace RazorPagesPizza.Repositories;

public class PizzaRepository : IPizzaRepository
{ 
    private static readonly string s_pizzasContainerName = "RazorPagesPizzaDb";
    private readonly CosmosClient _cosmosClient;

    public PizzaRepository(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
    }

    public async IAsyncEnumerable<Pizza> GetAllAsync()
    {
        using var feed = GetContainer().GetItemQueryIterator<Pizza>(
            new QueryDefinition("SELECT * FROM Pizza"),
            requestOptions: new QueryRequestOptions
            {
                PartitionKey = new PartitionKey(nameof(Pizza)),
            });
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();

            foreach (var item in response)
            {
                yield return item;
            }
        }
    }

    public async ValueTask<Pizza?> GetAsync(string id)
    {
        var item = await GetContainer().ReadItemAsync<Pizza>(
            id,
            new PartitionKey(nameof(Pizza)));
        return item.StatusCode == HttpStatusCode.NotFound ? null : item.Resource;
    }

    public async ValueTask AddAsync(Pizza pizza)
    {
        pizza.Id = Guid.NewGuid().ToString();
        await GetContainer().CreateItemAsync(pizza, new PartitionKey(nameof(Pizza)));
    }

    public async ValueTask DeleteAsync(string id) =>
        await GetContainer().DeleteItemAsync<Pizza>(id, new PartitionKey(nameof(Pizza)));

    public async ValueTask UpdateAsync(Pizza pizza)
    {
        var item = await GetAsync(pizza.Id);
        if (item is null) return;

        await GetContainer().ReplaceItemAsync(pizza, pizza.Id, new PartitionKey(nameof(Pizza)));
    }

    private Container GetContainer() => _cosmosClient.GetContainer(s_pizzasContainerName, nameof(Pizza));
}
