using FFPipeline.FFmpeg.InputOption;

namespace FFPipeline.FFmpeg.Decoder;

public interface IDecoder : IInputOption
{
    string Name { get; }
}
