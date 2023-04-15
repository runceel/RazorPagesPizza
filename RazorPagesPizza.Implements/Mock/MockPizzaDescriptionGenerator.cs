using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorPagesPizza.Domain.Repositories;

namespace RazorPagesPizza.Repositories.Mock;

public class MockPizzaDescriptionGenerator : IPizzaDescriptionGenerator
{
    public async ValueTask<GenerateOutput> GenerateAsync(GenerateInput input)
    {
        await Task.Delay(3000);
        return new GenerateOutput($"{input.PizzaName} はね！！！すっごい美味しくて食べたら幸せな気分になれるんだよ！！おすすめだよ！！");
    }
}
