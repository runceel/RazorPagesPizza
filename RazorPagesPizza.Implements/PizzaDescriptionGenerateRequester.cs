using Azure.Storage.Queues;
using RazorPagesPizza.Core.Services;
using RazorPagesPizza.Domain.Repositories;
using RazorPagesPizza.Models;
using RazorPagesPizza.Repositories;
using System.Text.Json;

namespace RazorPagesPizza.Implements.Repositories;

public class PizzaDescriptionGenerateRequester : IPizzaDescriptionGenerateRequester
{
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

        var queueClient = _queueServiceClient.GetQueueClient(PizzaQueue.Name);
        await queueClient.CreateIfNotExistsAsync();
        await queueClient.SendMessageAsync(
            JsonSerializer.Serialize(
                new GeneratePizzaDescriptionRequest { PizzaId = pizza.Id, PizzaName = pizza.Name! }, 
                SourceGenerationContext.Default.GeneratePizzaDescriptionRequest));
    }
}

<<<<<<< HEAD
public class GeneratePizzaDescriptionRequest
{
    public string PizzaId { get; set; } = "";
    public string PizzaName { get; set; } = "";
}
=======
public record GeneratePizzaDescriptionRequest(string PizzaId, string PizzaName);
>>>>>>> 04eff41e986823ee2e3c579ae5da74ac2cce44d6

public static class PizzaQueue
{
    public const string Name = "generate-pizza-description";
}
