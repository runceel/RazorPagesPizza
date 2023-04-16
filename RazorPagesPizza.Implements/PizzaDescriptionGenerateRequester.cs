using Azure.Storage.Queues;
using RazorPagesPizza.Core.Services;
using RazorPagesPizza.Domain.Repositories;
using RazorPagesPizza.Interfaces.Models;
using RazorPagesPizza.Repositories;
using System.Text;
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
        var json = new GeneratePizzaDescriptionRequest
        {
            PizzaId = pizza.Id,
            PizzaName = pizza.Name,
        }.ToJson();
        await queueClient.SendMessageAsync(Convert.ToBase64String(Encoding.UTF8.GetBytes(json)));
    }
}

public class GeneratePizzaDescriptionRequest
{
    public string PizzaId { get; set; } = "";
    public string PizzaName { get; set; } = "";

    public string ToJson() =>
        JsonSerializer.Serialize(
            this,
            SourceGenerationContext.Default.GeneratePizzaDescriptionRequest);

    public static GeneratePizzaDescriptionRequest? FromJson(string json) =>
        JsonSerializer.Deserialize(
            json,
            SourceGenerationContext.Default.GeneratePizzaDescriptionRequest);

    public static async ValueTask<GeneratePizzaDescriptionRequest?> FromJsonAsync(Stream utf8Stream) =>
        await JsonSerializer.DeserializeAsync(
            utf8Stream,
            SourceGenerationContext.Default.GeneratePizzaDescriptionRequest);
}

public static class PizzaQueue
{
    public const string Name = "generate-pizza-description";
}
