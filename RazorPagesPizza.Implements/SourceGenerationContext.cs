using RazorPagesPizza.Implements.Repositories;
using System.Text.Json.Serialization;

namespace RazorPagesPizza.Repositories;

[JsonSerializable(typeof(GeneratePizzaDescriptionRequest))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
