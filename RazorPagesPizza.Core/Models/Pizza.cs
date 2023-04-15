using System.ComponentModel.DataAnnotations;

namespace RazorPagesPizza.Interfaces.Models;

public class Pizza
{
    public string Id { get; set; } = "";

    [Required]
    public string? Name { get; set; }
    public PizzaSize Size { get; set; }
    public bool IsGlutenFree { get; set; }

    [Range(0.01, 9999.99)]
    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string PartitionKey => "Pizza";
}

public enum PizzaSize { Small, Medium, Large }
