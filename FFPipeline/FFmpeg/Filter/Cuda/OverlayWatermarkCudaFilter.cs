using FFPipeline.FFmpeg.Format;
using FFPipeline.FFmpeg.State;
using Microsoft.Extensions.Logging;

namespace FFPipeline.FFmpeg.Filter.Cuda;

public class OverlayWatermarkCudaFilter : OverlayWatermarkFilter
{
    public OverlayWatermarkCudaFilter(
        WatermarkState watermarkState,
        FrameSize resolution,
        FrameSize squarePixelFrameSize,
        ILogger logger) : base(
        watermarkState,
        resolution,
        squarePixelFrameSize,
        new PixelFormatUnknown(),
        logger)
    {
    }

    public override string Filter => $"overlay_cuda={Position}";

    public override FrameState NextState(FrameState currentState) =>
        currentState with { FrameDataLocation = FrameDataLocation.Hardware };
}
