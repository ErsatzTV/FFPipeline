using System.Text.Json.Serialization;

namespace FFPipeline.Commands;

record CapabilitiesInput
{
    [JsonPropertyName("ffmpegPath")]
    public string? FFmpegPath { get; set; }
}