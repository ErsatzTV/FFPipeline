using FFPipeline.FFmpeg.Format;

namespace FFPipeline.FFmpeg.Capabilities;

public class NoHardwareCapabilities : IHardwareCapabilities
{
    public FFmpegCapability CanDecode(
        string videoFormat,
        Option<string> videoProfile,
        Option<IPixelFormat> maybePixelFormat) =>
        FFmpegCapability.Software;

    public FFmpegCapability CanEncode(
        string videoFormat,
        Option<string> videoProfile,
        Option<IPixelFormat> maybePixelFormat) =>
        FFmpegCapability.Software;

    public Option<RateControlMode> GetRateControlMode(string videoFormat, Option<IPixelFormat> maybePixelFormat) =>
        Option<RateControlMode>.None;
}
