using System.Text.Json.Serialization;
using ConsoleAppFramework;
using FFPipeline.Commands;

var app = ConsoleApp.Create();

app.Add("capabilities", async (CancellationToken cancellationToken) =>
{
    await new CapabiltiesCommand().Run(cancellationToken);
});

await app.RunAsync(args);

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(CapabilitiesInput))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}