namespace FFPipeline.FFmpeg;

public interface IPipelineFilterStep : IPipelineStep
{
    string Filter { get; }
}
