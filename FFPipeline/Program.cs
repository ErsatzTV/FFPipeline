using System.Text.Json.Serialization;
using ConsoleAppFramework;
using ErsatzTV.FFmpeg.Capabilities;
using ErsatzTV.FFmpeg.Runtime;
using FFPipeline.Commands;
using FFPipeline.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZLogger;

var services = new ServiceCollection();

services.AddSingleton<IHardwareCapabilitiesFactory, HardwareCapabilitiesFactory>();
services.AddSingleton<IMemoryCache>(new MemoryCache(new MemoryCacheOptions()));
services.AddSingleton<IRuntimeInfo, RuntimeInfo>();

services.AddLogging(x =>
{
    x.ClearProviders();
    x.SetMinimumLevel(LogLevel.Trace);
    x.AddZLoggerConsole();
});

await using var serviceProvider = services.BuildServiceProvider();
ConsoleApp.ServiceProvider = serviceProvider;

var app = ConsoleApp.Create();

app.Add<CapabilitiesCommand>();

await app.RunAsync(args);

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(CapabilitiesInput))]
[JsonSerializable(typeof(FFmpegCapabilitiesModel))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}