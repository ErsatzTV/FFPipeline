using System.Diagnostics.CodeAnalysis;

namespace FFPipeline.FFmpeg;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum RateControlMode
{
    CBR,
    CQP,
    VBR
}
