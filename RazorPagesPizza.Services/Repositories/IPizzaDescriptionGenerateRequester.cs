using RazorPagesPizza.Interfaces.Models;

namespace RazorPagesPizza.Domain.Repositories;

public interface IPizzaDescriptionGenerateRequester
{
    ValueTask RequestToGeneratePizzaDescriptionAsync(Pizza pizza);
}
