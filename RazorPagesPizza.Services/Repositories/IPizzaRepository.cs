using RazorPagesPizza.Interfaces.Models;

namespace RazorPagesPizza.Domain.Repositories;

public interface IPizzaRepository
{
    ValueTask AddAsync(Pizza pizza);
    ValueTask DeleteAsync(string id);
    IAsyncEnumerable<Pizza> GetAllAsync();
    ValueTask<Pizza?> GetAsync(string id);
    ValueTask UpdateAsync(Pizza pizza);
}
