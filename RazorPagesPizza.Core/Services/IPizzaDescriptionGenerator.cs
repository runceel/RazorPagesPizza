using RazorPagesPizza.Models;

namespace RazorPagesPizza.Services;

public interface IPizzaDescriptionGenerator
{
    ValueTask RequestToGeneratePizzaDescriptionAsync(Pizza pizza);
}
