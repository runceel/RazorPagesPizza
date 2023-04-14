using RazorPagesPizza.Models;

namespace RazorPagesPizza.Domain.Repositories;

public interface IPizzaDescriptionGenerateRequester
{
    ValueTask RequestToGeneratePizzaDescriptionAsync(Pizza pizza);
}
