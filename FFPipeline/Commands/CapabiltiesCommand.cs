namespace FFPipeline.Commands;

public class CapabiltiesCommand
{
    public async Task Run(CancellationToken cancellationToken)
    {
        if (Console.IsInputRedirected)
        {
            var all = await Console.In.ReadToEndAsync(cancellationToken);

            var json = JsonExtensions.Deserialize<CapabilitiesInput>(all, SourceGenerationContext.Default);

            if (json != null)
            {
                Console.WriteLine("Input: " + json.FFmpegPath);
            }
        }
        else
        {
            Console.WriteLine("No input");
        }

        Console.WriteLine("Capabilities are ...");
    }
}