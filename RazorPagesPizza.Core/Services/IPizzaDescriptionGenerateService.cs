namespace RazorPagesPizza.Core.Services;

public interface IPizzaDescriptionGenerateService
{
    ValueTask GenerateDescriptionAsync(GenerateDescriptionInput input);
}

public record GenerateDescriptionInput(string PizzaId, string PizzaName);

