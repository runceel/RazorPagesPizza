using RazorPagesPizza.Models;

namespace RazorPagesPizza.Services;
public class PizzaService
{
    private List<Pizza> Pizzas { get; }
    private int nextId = 3;
    public PizzaService()
    {
        Pizzas = new List<Pizza>
                {
                    new Pizza { Id = 1, Name = "Classic Italian", Price=20.00M, Size=PizzaSize.Large, IsGlutenFree = false },
                    new Pizza { Id = 2, Name = "Veggie", Price=15.00M, Size=PizzaSize.Small, IsGlutenFree = true }
                };
    }

    public List<Pizza> GetAll() => Pizzas;

    public Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

    public void Add(Pizza pizza)
    {
        pizza.Id = nextId++;
        Pizzas.Add(pizza);
    }

    public void Delete(int id)
    {
        var pizza = Get(id);
        if (pizza is null)
            return;

        Pizzas.Remove(pizza);
    }

    public void Update(Pizza pizza)
    {
        var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
        if (index == -1)
            return;

        Pizzas[index] = pizza;
    }
}