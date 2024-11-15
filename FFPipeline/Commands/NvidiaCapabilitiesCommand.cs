using FFPipeline.FFmpeg.Capabilities;
using ConsoleAppFramework;
using FFPipeline.FFmpeg;

namespace FFPipeline.Commands;

public class NvidiaCapabilitiesCommand(IHardwareCapabilitiesFactory hardwareCapabilitiesFactory)
    : FFmpegCapabilitiesCommand(hardwareCapabilitiesFactory)
{
    [Command("nvidia-capabilities")]
    public override async Task Run([JsonValueParserAttribute<CapabilitiesRequest>] CapabilitiesRequest? input = null,
        CancellationToken cancellationToken = default)
    {
        var outJson = await (input ?? await GetRequest(cancellationToken))
            .MapAsync(GetFFmpegCapabilities)
            .MapAsync(GetNvidiaCapabilities)
            .ToOption()
            .Map(flatten)
            .MapAsync(capabilities => JsonExtensions.Serialize(capabilities.ToModel(), SourceGenerationContext.Default))
            .ToOption()
            .IfNoneAsync("{}");

        Console.WriteLine(outJson);
    }

    private async Task<Option<NvidiaHardwareCapabilities>> GetNvidiaCapabilities(Option<IFFmpegCapabilities> maybeCapabilities)
    {
        foreach (var ffmpegCapabilities in maybeCapabilities)
        {
            var nvidiaOutput = await HardwareCapabilitiesFactory.GetHardwareCapabilities(
                ffmpegCapabilities,
                HardwareAccelerationMode.Nvenc,
                Option<string>.None,
                Option<string>.None) as NvidiaHardwareCapabilities;

            return nvidiaOutput;
        }

        return Option<NvidiaHardwareCapabilities>.None;
    }
}