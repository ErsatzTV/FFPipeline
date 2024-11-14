namespace FFPipeline.FFmpeg.OutputOption.Metadata;

public class DoNotMapMetadataOutputOption : OutputOption
{
    public override string[] OutputOptions => new[] { "-map_metadata", "-1" };
}
