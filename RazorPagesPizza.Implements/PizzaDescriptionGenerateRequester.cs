using Azure.Storage.Queues;
using RazorPagesPizza.Core.Services;
using RazorPagesPizza.Models;
using RazorPagesPizza.Repositories;
using System.Text.Json;

namespace RazorPagesPizza.Implements.Repositories;

public class PizzaDescriptionGenerateRequester : IPizzaDescriptionGenerateRequester
{
    private static readonly string s_queueName = "generate-pizza-description";
    private readonly QueueServiceClient _queueServiceClient;

    public PizzaDescriptionGenerateRequester(QueueServiceClient queueServiceClient)
    {
        _queueServiceClient = queueServiceClient;
    }

    public async ValueTask RequestToGeneratePizzaDescriptionAsync(Pizza pizza)
    {
        if (string.IsNullOrWhiteSpace(pizza.Name))
        {
            // ピザに名前がない場合は何もしない
            return;
        }

        var queueClient = _queueServiceClient.GetQueueClient(s_queueName);
        await queueClient.CreateIfNotExistsAsync();
        await queueClient.SendMessageAsync(
            JsonSerializer.Serialize(
                new GeneratePizzaDescriptionRequest(pizza.Id, pizza.Name!), 
                SourceGenerationContext.Default.GeneratePizzaDescriptionRequest));
    }
}

public record GeneratePizzaDescriptionRequest(string PizzaId, string PizzaName);
