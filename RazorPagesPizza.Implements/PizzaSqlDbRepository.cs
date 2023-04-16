using Microsoft.EntityFrameworkCore;
using RazorPagesPizza.Domain.Repositories;
using RazorPagesPizza.Interfaces.Models;
using RazorPagesPizza.Repositories.Models;

namespace RazorPagesPizza.Repositories;

public class PizzaSqlDbRepository : IPizzaRepository
{
    private readonly IDbContextFactory<PizzaContext> _factory;

    public PizzaSqlDbRepository(IDbContextFactory<PizzaContext> factory)
    {
        _factory = factory;
    }

    public async ValueTask AddAsync(Pizza pizza)
    {
        await using var context = await _factory.CreateDbContextAsync();
        // 多分 SQL Server 的には GUID を主キーにするのはあんまりよくなかった気がする
        pizza.Id = Guid.NewGuid().ToString();
        await context.AddAsync(pizza);
        await context.SaveChangesAsync();
    }

    public async ValueTask DeleteAsync(string id)
    {
        await using var context = await _factory.CreateDbContextAsync();
        var target = await context.PizzaInventories.FirstOrDefaultAsync(x => x.Id == id);
        if (target is not null)
        {
            context.PizzaInventories.Remove(target);
            await context.SaveChangesAsync();
        }
    }

    public async IAsyncEnumerable<Pizza> GetAllAsync()
    {
        await using var context = await _factory.CreateDbContextAsync();
        await foreach (var pizza in context.PizzaInventories.AsAsyncEnumerable())
        {
            yield return pizza;
        }
    }

    public async ValueTask<Pizza?> GetAsync(string id)
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.PizzaInventories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async ValueTask UpdateAsync(Pizza pizza)
    {
        await using var context = await _factory.CreateDbContextAsync();
        context.PizzaInventories.Attach(pizza).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}
