using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RazorPagesPizza.Core.Services;
using RazorPagesPizza.Implements.Repositories;

namespace RazorPagesPizza.Functions
{
    public class GeneratePizzaDescriptionFunction
    {
        private readonly ILogger _logger;
        private readonly IPizzaDescriptionGenerateService _pizzaDescriptionGenerateService;

        public GeneratePizzaDescriptionFunction(IPizzaDescriptionGenerateService pizzaDescriptionGenerateService, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GeneratePizzaDescriptionFunction>();
            _pizzaDescriptionGenerateService = pizzaDescriptionGenerateService;
        }

        [Function("GeneratePizzaDescriptionFunction")]
        public async Task Run([QueueTrigger(PizzaQueue.Name, Connection = "PizzaQueue")] GeneratePizzaDescriptionRequest request)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {request.PizzaId}");
            await _pizzaDescriptionGenerateService.GenerateDescriptionAsync(new(request.PizzaId, request.PizzaName));
        }
    }
}
