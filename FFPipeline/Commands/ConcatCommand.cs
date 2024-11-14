using ConsoleAppFramework;
using FFPipeline.FFmpeg;
using FFPipeline.FFmpeg.Pipeline;
using FFPipeline.Models;

namespace FFPipeline.Commands;

public class ConcatCommand(IPipelineBuilderFactory pipelineBuilderFactory)
{
    [Command("concat")]
    public async Task Run(CancellationToken cancellationToken)
    {
        var maybeRequest = await GetRequest(cancellationToken);
        foreach (var request in maybeRequest)
        {
            if (request.Input?.Url is null || request.FFmpegPath is null)
            {
                break;
            }

            var resolution = new FrameSize(request.Input.Width, request.Input.Height);
            var concatInputFile = new ConcatInputFile(request.Input.Url, resolution);
            var pipelineBuilder = await pipelineBuilderFactory.GetBuilder(
                HardwareAccelerationMode.None,
                Option<VideoInputFile>.None,
                Option<AudioInputFile>.None,
                Option<WatermarkInputFile>.None,
                Option<SubtitleInputFile>.None,
                concatInputFile,
                Option<string>.None,
                Option<string>.None,
                null, // TODO: reports folder
                null, // TODO: fonts folder
                request.FFmpegPath);

            // TODO: saveReports
            var pipeline = pipelineBuilder.Concat(
                concatInputFile,
                FFmpegState.Concat(
                    false,
                    request.Metadata?.ServiceProvider ?? "FFPipeline",
                    request.Metadata?.ServiceName ?? string.Empty));

            var environmentVariables =
                CommandGenerator.GenerateEnvironmentVariables(pipeline.PipelineSteps);

            var arguments = CommandGenerator.GenerateArguments(
                Option<VideoInputFile>.None,
                Option<AudioInputFile>.None,
                Option<WatermarkInputFile>.None,
                concatInputFile,
                pipeline.PipelineSteps,
                pipeline.IsIntelVaapiOrQsv);

            var model = new PipelineModel
            {
                Environment = environmentVariables.Select(ev => new PipelineEnvironmentModel
                {
                    Key = ev.Key,
                    Value = ev.Value
                }),

                Arguments = arguments
            };

            Console.WriteLine(JsonExtensions.Serialize(model, SourceGenerationContext.Default));
        }

        if (maybeRequest.IsNone)
        {
            Console.WriteLine("{}");
        }
    }

    private static async Task<Option<ConcatRequest>> GetRequest(CancellationToken cancellationToken)
    {
        if (Console.IsInputRedirected)
        {
            var json = await Console.In.ReadToEndAsync(cancellationToken);
            var concatRequest = JsonExtensions.Deserialize<ConcatRequest>(json, SourceGenerationContext.Default);
            if (concatRequest != null)
            {
                return concatRequest;
            }
        }

        return Option<ConcatRequest>.None;
    }
}
