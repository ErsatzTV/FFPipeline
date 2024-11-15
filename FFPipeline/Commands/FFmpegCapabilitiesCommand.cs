using FFPipeline.FFmpeg.Capabilities;
using ConsoleAppFramework;

namespace FFPipeline.Commands;

public class FFmpegCapabilitiesCommand(IHardwareCapabilitiesFactory hardwareCapabilitiesFactory)
{
    protected IHardwareCapabilitiesFactory HardwareCapabilitiesFactory => hardwareCapabilitiesFactory;

    [Command("ffmpeg-capabilities")]
    public virtual async Task Run([JsonValueParserAttribute<CapabilitiesRequest>] CapabilitiesRequest? input = null,
        CancellationToken cancellationToken = default)
    {
        var outJson = await (input ?? await GetRequest(cancellationToken))
            .MapAsync(GetFFmpegCapabilities)
            .ToOption()
            .Map(flatten)
            .MapAsync(capabilities => JsonExtensions.Serialize(capabilities.ToModel(), SourceGenerationContext.Default))
            .ToOption()
            .IfNoneAsync("{}");

        Console.WriteLine(outJson);
    }

    protected static async Task<Option<CapabilitiesRequest>> GetRequest(CancellationToken cancellationToken)
    {
        if (Console.IsInputRedirected)
        {
            var json = await Console.In.ReadToEndAsync(cancellationToken);
            return Optional(JsonExtensions.Deserialize<CapabilitiesRequest>(json, SourceGenerationContext.Default));
        }

        return Option<CapabilitiesRequest>.None;
    }

    protected async Task<Option<IFFmpegCapabilities>> GetFFmpegCapabilities(CapabilitiesRequest request)
    {
        return await Some(request)
            .Map(input => Optional(input.FFmpegPath))
            .Flatten()
            .Filter(ffmpegPath => !string.IsNullOrEmpty(ffmpegPath))
            .Filter(File.Exists)
            .MapAsync(hardwareCapabilitiesFactory.GetFFmpegCapabilities)
            .ToOption();
    }
}