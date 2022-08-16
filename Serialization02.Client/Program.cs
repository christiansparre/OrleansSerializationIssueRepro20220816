using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Serialization02.Grains.Interfaces;

var builder = Host.CreateDefaultBuilder();

builder.ConfigureServices(services =>
{
    services.AddOrleansClient(client =>
    {
        client.UseLocalhostClustering();
        client.Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "Local";
            options.ServiceId = "Serialization02";
        });
    });
});

var host = builder.Build();

var _ = host.RunAsync();

var clusterClient = host.Services.GetRequiredService<IClusterClient>();

await Task.Delay(1000);

var grain = clusterClient.GetGrain<IHelloWorldGrain>(IHelloWorldGrain.Key);

while (true)
{
    Console.WriteLine("Press enter to continue!");
    var readLine = Console.ReadLine();

    if (readLine is not null && readLine == "quit")
    {
        break;
    }

    try
    {
        var r = await grain.Foo();
        Console.WriteLine($"{r.GetType().Name}: {r}");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    try
    {
        var r = await grain.Bar();
        Console.WriteLine($"{r.GetType().Name}: {r}");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    try
    {
        var r = await grain.Baz();
        Console.WriteLine($"{r.GetType().Name}: {r}");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    try
    {
        var r = await grain.FooBarOrBaz<Foo>();
        Console.WriteLine($"{r.GetType().Name}: {r}");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

Console.WriteLine("Bye!");

await host.StopAsync(TimeSpan.FromSeconds(2000));
