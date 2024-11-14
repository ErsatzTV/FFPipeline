namespace FFPipeline.FFmpeg;

public record FFmpegPipeline(IList<IPipelineStep> PipelineSteps, bool IsIntelVaapiOrQsv);
