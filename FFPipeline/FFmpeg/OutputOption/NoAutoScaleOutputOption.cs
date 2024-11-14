namespace FFPipeline.FFmpeg.OutputOption;

public class NoAutoScaleOutputOption : OutputOption
{
    public override string[] OutputOptions => new[] { "-noautoscale" };
}
