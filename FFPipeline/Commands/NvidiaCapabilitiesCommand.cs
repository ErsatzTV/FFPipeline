using FFPipeline.FFmpeg.Capabilities;
using ConsoleAppFramework;
using FFPipeline.FFmpeg;

namespace FFPipeline.Commands;

public class NvidiaCapabilitiesCommand : FFmpegCapabilitiesCommand
{
    public NvidiaCapabilitiesCommand(IHardwareCapabilitiesFactory hardwareCapabilitiesFactory) : base(
        hardwareCapabilitiesFactory)
    {
    }

    [Command("nvidia-capabilities")]
    public override async Task Run(CancellationToken cancellationToken)
    {
        var maybeRequest = await GetRequest(cancellationToken);
        var maybeCapabilities = await GetFFmpegCapabilities(maybeRequest);
        var maybeNvidiaCapabilities = await GetNvidiaCapabilities(maybeRequest, maybeCapabilities);
        foreach (var nvidiaCapabilities in maybeNvidiaCapabilities)
        {
            var modelJson = JsonExtensions.Serialize(nvidiaCapabilities.ToModel(), SourceGenerationContext.Default);
            Console.WriteLine(modelJson);
        }

        if (maybeCapabilities.IsNone)
        {
            Console.WriteLine("{}");
        }
    }

    private async Task<Option<NvidiaHardwareCapabilities>> GetNvidiaCapabilities(
        Option<CapabilitiesRequest> maybeRequest,
        Option<IFFmpegCapabilities> maybeCapabilities)
    {
        foreach (var request in maybeRequest)
        {
            foreach (var ffmpegCapabilities in maybeCapabilities)
            {
                if (!string.IsNullOrEmpty(request.FFmpegPath) && File.Exists(request.FFmpegPath))
                {
                    var nvidiaOutput = await HardwareCapabilitiesFactory.GetHardwareCapabilities(
                        ffmpegCapabilities,
                        request.FFmpegPath,
                        HardwareAccelerationMode.Nvenc,
                        Option<string>.None,
                        Option<string>.None) as NvidiaHardwareCapabilities;

                    return nvidiaOutput;
                }
            }
        }

        return Option<NvidiaHardwareCapabilities>.None;
    }
}