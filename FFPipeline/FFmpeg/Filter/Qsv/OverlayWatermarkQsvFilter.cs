using FFPipeline.FFmpeg.Format;
using FFPipeline.FFmpeg.State;
using Microsoft.Extensions.Logging;

namespace FFPipeline.FFmpeg.Filter.Qsv;

public class OverlayWatermarkQsvFilter : OverlayWatermarkFilter
{
    public OverlayWatermarkQsvFilter(
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

    public override string Filter => $"overlay_qsv={Position}";

    public override FrameState NextState(FrameState currentState) =>
        currentState with { FrameDataLocation = FrameDataLocation.Hardware };
}
