using FFPipeline.FFmpeg.Format;

namespace FFPipeline.FFmpeg.Encoder;

public class EncoderRawVideo : EncoderBase
{
    public override string Name => "rawvideo";

    public override StreamKind Kind => StreamKind.Video;

    public override FrameState NextState(FrameState currentState) =>
        currentState with { VideoFormat = VideoFormat.Raw };
}
