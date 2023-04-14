namespace RazorPagesPizza.Repositories.Options;

public class OpenAIOptions
{
    public required string Endpoint { get; set; }
    public required string Key { get; set; }
    public required string DeployModelName { get; set; }
}
