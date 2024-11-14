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
    public override async Task Run([JsonValueParserAttribute<CapabilitiesInput>] CapabilitiesInput? input, CancellationToken cancellationToken)
    {
        var maybeInput = input ?? await GetInput(cancellationToken);
        var outJson = maybeInput
        .MapAsync(maybeInput =>
        {
            return GetFFmpegCapabilities(maybeInput, cancellationToken)
                            .MapAsync(capabilities => GetNvidiaCapabilities(maybeInput, capabilities));
        })
        .Map(x => x.Match(nvidiaCapabilities => JsonExtensions.Serialize(nvidiaCapabilities.ToModel(), SourceGenerationContext.Default), "{}"));

        await foreach (var json in outJson)
        {
            Console.WriteLine(json);
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