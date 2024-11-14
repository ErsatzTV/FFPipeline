using System.Text.Json.Serialization;

namespace FFPipeline.Models;

public class FFmpegCapabilitiesModel
{
    [JsonPropertyName("hwAccels")]
    public string[] HwAccels { get; set; } = [];

    [JsonPropertyName("decoders")]
    public string[] Decoders { get; set; } = [];

    [JsonPropertyName("filters")]
    public string[] Filters { get; set; } = [];

    [JsonPropertyName("encoders")]
    public string[] Encoders { get; set; } = [];

    [JsonPropertyName("options")]
    public string[] Options { get; set; } = [];
}