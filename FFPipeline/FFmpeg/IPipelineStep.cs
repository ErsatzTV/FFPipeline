﻿using FFPipeline.FFmpeg.Environment;

namespace FFPipeline.FFmpeg;

public interface IPipelineStep
{
    EnvironmentVariable[] EnvironmentVariables { get; }
    string[] GlobalOptions { get; }
    string[] FilterOptions { get; }
    string[] OutputOptions { get; }
    string[] InputOptions(InputFile inputFile);

    FrameState NextState(FrameState currentState);
}
