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
    public virtual async Task Run(CancellationToken cancellationToken)
    {
        var maybeRequest = await GetRequest(cancellationToken);
        var maybeCapabilities = await GetFFmpegCapabilities(maybeRequest);
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

    protected async Task<Option<CapabilitiesRequest>> GetRequest(CancellationToken cancellationToken)
    {
        if (Console.IsInputRedirected)
        {
            var json = await Console.In.ReadToEndAsync(cancellationToken);
            var capabilitiesRequest = JsonExtensions.Deserialize<CapabilitiesRequest>(json, SourceGenerationContext.Default);
            if (capabilitiesRequest != null)
            {
                return capabilitiesRequest;
            }
        }

        return Option<CapabilitiesRequest>.None;
    }

    protected async Task<Option<IFFmpegCapabilities>> GetFFmpegCapabilities(
        Option<CapabilitiesRequest> maybeRequest)
    {
        foreach (var request in maybeRequest)
        {
            if (!string.IsNullOrEmpty(request.FFmpegPath) && File.Exists(request.FFmpegPath))
            {
                return Some(await _hardwareCapabilitiesFactory.GetFFmpegCapabilities(request.FFmpegPath));
            }
        }

        return Option<IFFmpegCapabilities>.None;
    }
}