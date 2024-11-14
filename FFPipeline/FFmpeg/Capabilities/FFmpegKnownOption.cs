using System.Diagnostics.CodeAnalysis;

namespace FFPipeline.FFmpeg.Capabilities;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public record FFmpegKnownOption
{
    public static readonly FFmpegKnownOption ReadrateInitialBurst = new("readrate_initial_burst");

    private FFmpegKnownOption(string Name) => this.Name = Name;

    public string Name { get; }

    public static IList<string> AllOptions =>
        new[]
        {
            ReadrateInitialBurst.Name
        };
}
