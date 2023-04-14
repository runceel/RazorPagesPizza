using RazorPagesPizza.Core.Services;
using RazorPagesPizza.Models;

namespace RazorPagesPizza.Services;
public class ManagePizzaInventoryService : IManagePizzaInventoryService
{
    private readonly IPizzaRepository _pizzaRepository;
    private readonly IPizzaDescriptionGenerateRequester _pizzaDescriptionGenerator;

    public ManagePizzaInventoryService(IPizzaRepository pizzaRepository, IPizzaDescriptionGenerateRequester pizzaDescriptionGenerator)
    {
        _pizzaRepository = pizzaRepository;
        _pizzaDescriptionGenerator = pizzaDescriptionGenerator;
    }

    public async ValueTask AddAsync(Pizza pizza)
    {
        await _pizzaRepository.AddAsync(pizza);
        await _pizzaDescriptionGenerator.RequestToGeneratePizzaDescriptionAsync(pizza);
    }

    public ValueTask DeleteAsync(string id) => _pizzaRepository.DeleteAsync(id);

    public IAsyncEnumerable<Pizza> GetAllAsync() => _pizzaRepository.GetAllAsync();

    public ValueTask<Pizza?> GetAsync(string id) => _pizzaRepository.GetAsync(id);

    public ValueTask UpdateAsync(Pizza pizza) => _pizzaRepository.UpdateAsync(pizza);
}