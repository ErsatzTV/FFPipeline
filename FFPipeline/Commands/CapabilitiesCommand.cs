using ErsatzTV.FFmpeg.Capabilities;
using ConsoleAppFramework;

namespace FFPipeline.Commands;

public class CapabilitiesCommand
{
    private readonly IHardwareCapabilitiesFactory _hardwareCapabilitiesFactory;

    public CapabilitiesCommand(IHardwareCapabilitiesFactory hardwareCapabilitiesFactory) =>
        _hardwareCapabilitiesFactory = hardwareCapabilitiesFactory;

    [Command("capabilities")]
    public async Task Run(CancellationToken cancellationToken)
    {
        if (Console.IsInputRedirected)
        {
            var all = await Console.In.ReadToEndAsync(cancellationToken);

            var json = JsonExtensions.Deserialize<CapabilitiesInput>(all, SourceGenerationContext.Default);

            if (json != null && File.Exists(json.FFmpegPath))
            {
                // TODO: remove memory cache from hardware capabilities factory?
                var capabilities = await _hardwareCapabilitiesFactory.GetFFmpegCapabilities(json.FFmpegPath);
                var modelJson = JsonExtensions.Serialize(capabilities.ToModel(), SourceGenerationContext.Default);
                Console.WriteLine(modelJson);
            }
        }
        else
        {
            Console.WriteLine("No input");
        }
    }
}