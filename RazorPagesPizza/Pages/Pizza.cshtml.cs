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

    public void OnGet()
    {
        Pizzas = _pizzaService.GetAll();
    }

    public string GlutenFreeText(Pizza pizza)
    {
        return pizza.IsGlutenFree ? "Gluten Free" : "Not Gluten Free";
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        _pizzaService.Add(NewPizza);
        return RedirectToAction("Get");
    }

    public IActionResult OnPostDelete(int id)
    {
        _pizzaService.Delete(id);
        return RedirectToAction("Get");
    }
}
