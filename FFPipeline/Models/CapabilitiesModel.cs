using System.Text.Json.Serialization;

namespace FFPipeline.Models;

public class CapabilitiesModel
{
    [JsonPropertyName("ffmpeg")]
    public FFmpegCapabilitiesModel FFmpeg { get; set; }

    [JsonPropertyName("nvidia")]
    public NvidiaCapabilitiesModel? Nvidia { get; set; }
}