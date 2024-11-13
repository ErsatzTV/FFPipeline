using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace FFPipeline;

public static partial class JsonExtensions
{
    public static string Serialize<TValue>(TValue value, IJsonTypeInfoResolver resolver, params JsonConverter [] converters)
    {
        var options = new JsonSerializerOptions
        {
            TypeInfoResolver = resolver,
        };
        foreach (var converter in converters)
            options.Converters.Add(converter);
        var jsonTypeInfo = (JsonTypeInfo<TValue>)options.GetTypeInfo(typeof(TValue));
        return JsonSerializer.Serialize(value, jsonTypeInfo);
    }

    public static TValue? Deserialize<TValue>(ReadOnlySpan<char> json, IJsonTypeInfoResolver resolver, params JsonConverter [] converters)
    {
        var options = new JsonSerializerOptions
        {
            TypeInfoResolver = resolver,
        };
        foreach (var converter in converters)
            options.Converters.Add(converter);
        var jsonTypeInfo = (JsonTypeInfo<TValue>)options.GetTypeInfo(typeof(TValue));
        return JsonSerializer.Deserialize(json, jsonTypeInfo);
    }
}