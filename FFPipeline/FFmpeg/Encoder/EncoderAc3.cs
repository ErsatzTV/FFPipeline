namespace FFPipeline.FFmpeg.Encoder;

public class EncoderAc3 : EncoderBase
{
    public override string Name => "ac3";

    public override StreamKind Kind => StreamKind.Audio;
}
