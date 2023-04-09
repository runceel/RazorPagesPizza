using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using RazorPagesPizza.Models;
using RazorPagesPizza.Services;

namespace RazorPagesPizza.Pages;

public class PizzaModel : PageModel
{
    private readonly PizzaService _pizzaService;

    public List<Pizza> Pizzas { get; private set; } = new();

    [BindProperty]
    public Pizza NewPizza { get; set; } = new();

    public PizzaModel(PizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    public async Task OnGetAsync()
    {
        var result = new List<Pizza>();
        await foreach (var pizza in _pizzaService.GetAllAsync())
        {
            result.Add(pizza);
        }

        Pizzas = result;
    }

    public string GlutenFreeText(Pizza pizza)
    {
        return pizza.IsGlutenFree ? "Gluten Free" : "Not Gluten Free";
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _pizzaService.AddAsync(NewPizza);
        return RedirectToAction("Get");
    }

    public async Task<IActionResult> OnPostDelete(string id)
    {
        await _pizzaService.DeleteAsync(id);
        return RedirectToAction("Get");
    }
}
