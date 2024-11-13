using ConsoleAppFramework;
using FFPipeline.Commands;

var app = ConsoleApp.Create();

app.Add("capabilities", async () => { await new CapabiltiesCommand().Run(); });

app.Run(args);
