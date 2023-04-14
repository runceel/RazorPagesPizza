﻿namespace RazorPagesPizza.Core.Services;

public interface IPizzaDescriptionGenerator
{
    ValueTask<GenerateOutput> GenerateAsync(GenerateInput input);
}

public record GenerateInput(string PizzaName);
public record GenerateOutput(string Description);
