using System.Text.Json.Serialization;

namespace FFPipeline.Models;

public class PipelineModel
{
    [JsonPropertyName("environment")]
    public PipelineEnvironmentModel[] Environment { get; set; } = [];

    [JsonPropertyName("arguments")]
    public string[] Arguments { get; set; } = [];
}

public class PipelineEnvironmentModel
{
    [JsonPropertyName("key")]
    public string? Key { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}