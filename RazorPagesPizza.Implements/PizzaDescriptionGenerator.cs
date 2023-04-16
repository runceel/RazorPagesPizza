using RazorPagesPizza.Domain.Repositories;

namespace RazorPagesPizza.Repositories;

public class PizzaDescriptionGenerator : IPizzaDescriptionGenerator
{
    public ValueTask<GenerateOutput> GenerateAsync(GenerateInput input)
    {
        throw new NotImplementedException();
    }
}
