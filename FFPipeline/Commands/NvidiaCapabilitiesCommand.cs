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
        var maybeInput = await GetInput(cancellationToken);
        var maybeCapabilities = await GetFFmpegCapabilities(maybeInput, cancellationToken);
        var maybeNvidiaCapabilities = await GetNvidiaCapabilities(maybeInput, maybeCapabilities);
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
        Option<CapabilitiesInput> maybeInput,
        Option<IFFmpegCapabilities> maybeCapabilities)
    {
        foreach (var input in maybeInput)
        {
            foreach (var ffmpegCapabilities in maybeCapabilities)
            {
                if (!string.IsNullOrEmpty(input.FFmpegPath) && File.Exists(input.FFmpegPath))
                {
                    var nvidiaOutput = await HardwareCapabilitiesFactory.GetHardwareCapabilities(
                        ffmpegCapabilities,
                        input.FFmpegPath,
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