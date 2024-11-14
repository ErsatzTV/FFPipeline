using System.Text.Json.Serialization;
using ConsoleAppFramework;
using FFPipeline.FFmpeg.Capabilities;
using FFPipeline.FFmpeg.Runtime;
using FFPipeline.Commands;
using FFPipeline.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZLogger;

var services = new ServiceCollection();

services.AddSingleton<IHardwareCapabilitiesFactory, HardwareCapabilitiesFactory>();
services.AddSingleton<IRuntimeInfo, RuntimeInfo>();

// TODO: do we need memory cache if this process is short lived?
services.AddSingleton<IMemoryCache>(new MemoryCache(new MemoryCacheOptions()));

services.AddLogging(x =>
{
    // min level and stderr threshold both warning, so nothing will interfere with stdout
    x.ClearProviders();
    x.SetMinimumLevel(LogLevel.Warning);
    x.AddZLoggerConsole(o => o.LogToStandardErrorThreshold = LogLevel.Warning);
});

await using var serviceProvider = services.BuildServiceProvider();
ConsoleApp.ServiceProvider = serviceProvider;

var app = ConsoleApp.Create();

app.Add<FFmpegCapabilitiesCommand>();
app.Add<NvidiaCapabilitiesCommand>();

await app.RunAsync(args);

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(CapabilitiesInput))]
[JsonSerializable(typeof(CapabilitiesModel))]
[JsonSerializable(typeof(FFmpegCapabilitiesModel))]
[JsonSerializable(typeof(NvidiaCapabilitiesModel))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}