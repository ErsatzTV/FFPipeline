using ConsoleAppFramework;

var app = ConsoleApp.Create();
app.Add("capabilities", async () =>
{
    await Task.Delay(100);
    Console.WriteLine("Capabilities...");
});

app.Run(args);
