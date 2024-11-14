namespace FFPipeline.FFmpeg.OutputOption;

public class ShortestOutputOption : OutputOption
{
    public override string[] OutputOptions => ["-shortest"];
}
