using System.Text.Json.Serialization;

namespace FFPipeline.Models;

public class NvidiaCapabilitiesModel
{
    [JsonPropertyName("architecture")]
    public int Architecture { get; set; }

    [JsonPropertyName("model")]
    public string? Model { get; set; }
}