using FFPipeline.FFmpeg.Format;
using FFPipeline.FFmpeg.OutputFormat;
using FFPipeline.FFmpeg.State;
using Microsoft.Extensions.Logging;

namespace FFPipeline.FFmpeg.Encoder;

public static class AvailableEncoders
{
    public static Option<IEncoder> ForAudioFormat(FFmpegState ffmpegState, AudioState desiredState, ILogger logger)
    {
        if (ffmpegState.OutputFormat is OutputFormatKind.Nut)
        {
            return new EncoderPcmS16Le();
        }

        return desiredState.AudioFormat.Match(
            audioFormat =>
                audioFormat switch
                {
                    AudioFormat.Aac => (Option<IEncoder>)new EncoderAac(),
                    AudioFormat.Ac3 => new EncoderAc3(),
                    AudioFormat.Copy => new EncoderCopyAudio(),
                    _ => LogUnknownEncoder(audioFormat, logger)
                },
            () => LogUnknownEncoder(string.Empty, logger));
    }

    private static Option<IEncoder> LogUnknownEncoder(
        string audioFormat,
        ILogger logger)
    {
        logger.LogWarning("Unable to determine audio encoder for {AudioFormat}; may have playback issues", audioFormat);
        return Option<IEncoder>.None;
    }
}
