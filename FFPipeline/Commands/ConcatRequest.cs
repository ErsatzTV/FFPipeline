using System.Text.Json.Serialization;

namespace FFPipeline.Commands;

public class ConcatRequest
{
    [JsonPropertyName("ffmpegPath")]
    public string? FFmpegPath { get; set; }

    [JsonPropertyName("input")]
    public ConcatRequestInput Input { get; set; }

    [JsonPropertyName("metadata")]
    public ConcatRequestMetadata Metadata { get; set; }
}

public class ConcatRequestInput
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }
}

public class ConcatRequestMetadata
{
    [JsonPropertyName("serviceName")]
    public string? ServiceName { get; set; }
}
