using FFPipeline.FFmpeg.Environment;

namespace FFPipeline.FFmpeg.OutputOption;

public abstract class OutputOption : IPipelineStep
{
    public static FrameDataLocation OutputFrameDataLocation => FrameDataLocation.Unknown;
    public EnvironmentVariable[] EnvironmentVariables => Array.Empty<EnvironmentVariable>();
    public string[] GlobalOptions => Array.Empty<string>();
    public string[] InputOptions(InputFile inputFile) => Array.Empty<string>();
    public string[] FilterOptions => Array.Empty<string>();
    public abstract string[] OutputOptions { get; }

    public virtual FrameState NextState(FrameState currentState) => currentState;
}
