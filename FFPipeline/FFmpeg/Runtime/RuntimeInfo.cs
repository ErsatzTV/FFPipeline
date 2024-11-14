using System.Runtime.InteropServices;

namespace FFPipeline.FFmpeg.Runtime;

public class RuntimeInfo : IRuntimeInfo
{
    public bool IsOSPlatform(OSPlatform osPlatform)
    {
        return RuntimeInformation.IsOSPlatform(osPlatform);
    }
}