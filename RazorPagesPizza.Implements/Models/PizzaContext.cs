using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RazorPagesPizza.Interfaces.Models;

namespace RazorPagesPizza.Repositories.Models;

public class PizzaContext : DbContext
{
    public DbSet<Pizza> PizzaInventories => Set<Pizza>();

    public PizzaContext(DbContextOptions<PizzaContext> options) : base(options)
    {
    }
}

public class DesignTimePizzaContextFactory : IDesignTimeDbContextFactory<PizzaContext>
{
    public PizzaContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PizzaContext>();
        optionsBuilder.UseSqlServer("Data Source=pizza.db");
        return new PizzaContext(optionsBuilder.Options);
    }
}
