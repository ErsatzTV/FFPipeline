﻿namespace FFPipeline.FFmpeg.GlobalOption;

public class LoglevelErrorOption : GlobalOption
{
    public override string[] GlobalOptions => new[] { "-loglevel", "error" };
}
