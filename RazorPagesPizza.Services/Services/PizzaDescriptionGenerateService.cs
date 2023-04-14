using RazorPagesPizza.Core.Services;
using RazorPagesPizza.Domain.Repositories;

namespace RazorPagesPizza.Domain.Services;

public class PizzaDescriptionGenerateService : IPizzaDescriptionGenerateService
{
    private readonly IPizzaDescriptionGenerator _pizzaDescriptionGenerator;
    private readonly IPizzaRepository _pizzaRepository;

    public PizzaDescriptionGenerateService(IPizzaDescriptionGenerator pizzaDescriptionGenerator, IPizzaRepository pizzaRepository)
    {
        _pizzaDescriptionGenerator = pizzaDescriptionGenerator;
        _pizzaRepository = pizzaRepository;
    }

    public async ValueTask GenerateDescriptionAsync(GenerateDescriptionInput input)
    {
        var generateResult = await _pizzaDescriptionGenerator.GenerateAsync(new(input.PizzaName));
        var pizza = await _pizzaRepository.GetAsync(input.PizzaId);
        if (pizza == null) { return; }

        pizza.Description = generateResult.Description;
        await _pizzaRepository.UpdateAsync(pizza);
    }
}
