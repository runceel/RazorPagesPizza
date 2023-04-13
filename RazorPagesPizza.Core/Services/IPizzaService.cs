using RazorPagesPizza.Models;

namespace RazorPagesPizza.Services;
public interface IPizzaService
{
    ValueTask AddAsync(Pizza pizza);
    ValueTask DeleteAsync(string id);
    IAsyncEnumerable<Pizza> GetAllAsync();
    ValueTask<Pizza?> GetAsync(string id);
    ValueTask UpdateAsync(Pizza pizza);
}