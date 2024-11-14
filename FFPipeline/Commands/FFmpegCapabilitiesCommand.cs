using FFPipeline.FFmpeg.Capabilities;
using ConsoleAppFramework;
namespace FFPipeline.Commands;

public class FFmpegCapabilitiesCommand
{
    private readonly IHardwareCapabilitiesFactory _hardwareCapabilitiesFactory;

    public FFmpegCapabilitiesCommand(IHardwareCapabilitiesFactory hardwareCapabilitiesFactory) =>
        _hardwareCapabilitiesFactory = hardwareCapabilitiesFactory;

    protected IHardwareCapabilitiesFactory HardwareCapabilitiesFactory => _hardwareCapabilitiesFactory;

    [Command("ffmpeg-capabilities")]
    public virtual async Task Run([JsonValueParserAttribute<CapabilitiesInput>] CapabilitiesInput? input, CancellationToken cancellationToken)
    {
        var outJson = (input ?? await GetInput(cancellationToken))
        .MapAsync(maybeInput => GetFFmpegCapabilities(maybeInput, cancellationToken))
        .ToOption()
        .Map(flatten)
        .MapAsync(ffmpegCapabilities => JsonExtensions.Serialize(ffmpegCapabilities.ToModel(), SourceGenerationContext.Default) ?? "{}");

        await foreach (var json in outJson)
        {
            Console.WriteLine(json);
        }
    }

    protected static async Task<Option<CapabilitiesInput>> GetInput(CancellationToken cancellationToken)
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
        CapabilitiesInput input,
        CancellationToken cancellationToken)
    {
        return await Option<CapabilitiesInput>.Some(input)
        .Map(input => Optional(input.FFmpegPath))
        .Flatten()
        .Filter(ffmpegPath => !string.IsNullOrEmpty(ffmpegPath))
        .Filter(File.Exists)
        .MapAsync(_hardwareCapabilitiesFactory.GetFFmpegCapabilities)
        .ToOption();
    }
}