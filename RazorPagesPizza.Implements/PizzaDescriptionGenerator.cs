using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorPagesPizza.Domain.Repositories;

namespace RazorPagesPizza.Repositories;

public class PizzaDescriptionGenerator : IPizzaDescriptionGenerator
{
    public ValueTask<GenerateOutput> GenerateAsync(GenerateInput input)
    {
        throw new NotImplementedException();
    }
}
