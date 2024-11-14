using FFPipeline.FFmpeg.Capabilities;
using ConsoleAppFramework;

namespace FFPipeline.Commands;

public class FFmpegCapabilitiesCommand
{
    private readonly IHardwareCapabilitiesFactory _hardwareCapabilitiesFactory;

    public FFmpegCapabilitiesCommand(IHardwareCapabilitiesFactory hardwareCapabilitiesFactory) =>
        _hardwareCapabilitiesFactory = hardwareCapabilitiesFactory;

    protected async Task<Option<CapabilitiesInput>> GetInput(CancellationToken cancellationToken)
    {
        if (Console.IsInputRedirected)
        {
            var json = await Console.In.ReadToEndAsync(cancellationToken);
            var capabilitiesInput = JsonExtensions.Deserialize<CapabilitiesInput>(json, SourceGenerationContext.Default);
            if (capabilitiesInput != null)
            {
                return capabilitiesInput;
            }
        }

        return Option<CapabilitiesInput>.None;
    }

    protected async Task<Option<IFFmpegCapabilities>> GetFFmpegCapabilities(
        Option<CapabilitiesInput> maybeInput,
        CancellationToken cancellationToken)
    {
        foreach (var input in maybeInput)
        {
            if (!string.IsNullOrEmpty(input.FFmpegPath) && File.Exists(input.FFmpegPath))
            {
                return Some(await _hardwareCapabilitiesFactory.GetFFmpegCapabilities(input.FFmpegPath));
            }
        }

        return Option<IFFmpegCapabilities>.None;
    }

    [Command("ffmpeg-capabilities")]
    public async Task Run(CancellationToken cancellationToken)
    {
        var maybeInput = await GetInput(cancellationToken);
        var maybeCapabilities = await GetFFmpegCapabilities(maybeInput, cancellationToken);
        foreach (var ffmpegCapabilities in maybeCapabilities)
        {
            var modelJson = JsonExtensions.Serialize(ffmpegCapabilities.ToModel(), SourceGenerationContext.Default);
            Console.WriteLine(modelJson);
        }

        if (maybeCapabilities.IsNone)
        {
            Console.WriteLine("{}");
        }
    }
}