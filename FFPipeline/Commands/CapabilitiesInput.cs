using System.Text.Json.Serialization;

namespace FFPipeline.Commands;

public class CapabilitiesInput
{
    [JsonPropertyName("ffmpegPath")]
    public string? FFmpegPath { get; set; }
}