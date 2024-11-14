using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace FFPipeline.FFmpeg.Runtime;

public interface IRuntimeInfo
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    bool IsOSPlatform(OSPlatform osPlatform);
}
