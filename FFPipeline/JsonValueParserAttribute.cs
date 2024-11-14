using ConsoleAppFramework;

namespace FFPipeline;

[AttributeUsage(AttributeTargets.Parameter)]
public class JsonValueParserAttribute<T> : Attribute, IArgumentParser<T>
{
    public static bool TryParse(ReadOnlySpan<char> input, out T result)
    {
        var inputFileString = input.ToString();
        if (!File.Exists(inputFileString))
        {
            result = default;
            return false;
        }

        var o = JsonExtensions.Deserialize<T>(File.ReadAllText(inputFileString), SourceGenerationContext.Default);
        if (o == null)
        {
            result = default;
            return false;
        }
        result = o;
        return true;
    }
}