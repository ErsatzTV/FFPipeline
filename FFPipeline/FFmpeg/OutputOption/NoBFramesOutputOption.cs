namespace FFPipeline.FFmpeg.OutputOption;

public class NoBFramesOutputOption : OutputOption
{
    public override string[] OutputOptions => ["-bf", "0"];
}
