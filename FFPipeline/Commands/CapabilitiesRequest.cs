using System.Text.Json.Serialization;

namespace FFPipeline.Commands;

public class CapabilitiesRequest
{
    [JsonPropertyName("ffmpegPath")]
    public string? FFmpegPath { get; set; }
}