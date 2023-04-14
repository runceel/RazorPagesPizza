using RazorPagesPizza.Models;

namespace RazorPagesPizza.Core.Services;

public interface IPizzaDescriptionGenerateRequester
{
    ValueTask RequestToGeneratePizzaDescriptionAsync(Pizza pizza);
}
