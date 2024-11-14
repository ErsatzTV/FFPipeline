using FFPipeline.FFmpeg.Capabilities;
using ConsoleAppFramework;
using FFPipeline.FFmpeg;
using FFPipeline.Models;

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
                var ffmpegCapabilities = await _hardwareCapabilitiesFactory.GetFFmpegCapabilities(json.FFmpegPath);
                var nvidiaCapabilities = await _hardwareCapabilitiesFactory.GetHardwareCapabilities(ffmpegCapabilities,
                        json.FFmpegPath, HardwareAccelerationMode.Nvenc, Option<string>.None, Option<string>.None)
                    as NvidiaHardwareCapabilities;

                var model = new CapabilitiesModel
                {
                    FFmpeg = ffmpegCapabilities.ToModel(),
                    Nvidia = nvidiaCapabilities?.ToModel(),
                };

                var modelJson = JsonExtensions.Serialize(model, SourceGenerationContext.Default);
                Console.WriteLine(modelJson);
            }
        }
        else
        {
            Console.WriteLine("No input");
        }
    }
}