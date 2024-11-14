using System.Globalization;
using FFPipeline.FFmpeg.Environment;

namespace FFPipeline.FFmpeg.OutputOption;

public class FrameRateOutputOption : IPipelineStep
{
    private readonly int _frameRate;

    public FrameRateOutputOption(int frameRate) => _frameRate = frameRate;

    public EnvironmentVariable[] EnvironmentVariables => Array.Empty<EnvironmentVariable>();
    public string[] GlobalOptions => Array.Empty<string>();
    public string[] InputOptions(InputFile inputFile) => Array.Empty<string>();
    public string[] FilterOptions => Array.Empty<string>();

    public string[] OutputOptions => new[]
        { "-r", _frameRate.ToString(CultureInfo.InvariantCulture), "-vsync", "cfr" };

    public FrameState NextState(FrameState currentState) => currentState with
    {
        FrameRate = _frameRate
    };
}
