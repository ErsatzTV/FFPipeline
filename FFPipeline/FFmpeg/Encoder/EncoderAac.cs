namespace FFPipeline.FFmpeg.Encoder;

public class EncoderAac : EncoderBase
{
    public override string Name => "aac";

    public override StreamKind Kind => StreamKind.Audio;
}
