using RazorPagesPizza.Implements.Repositories;
using System.Text.Json.Serialization;

namespace RazorPagesPizza.Implements;

[JsonSerializable(typeof(GeneratePizzaDescriptionRequest))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
