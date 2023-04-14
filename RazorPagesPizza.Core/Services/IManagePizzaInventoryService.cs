﻿using RazorPagesPizza.Models;

namespace RazorPagesPizza.Core.Services;
public interface IManagePizzaInventoryService
{
    ValueTask AddAsync(Pizza pizza);
    ValueTask DeleteAsync(string id);
    IAsyncEnumerable<Pizza> GetAllAsync();
    ValueTask<Pizza?> GetAsync(string id);
    ValueTask UpdateAsync(Pizza pizza);
}