using System.Text.Json;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using Serialization02.Grains.Interfaces;

namespace Serialization02.Grains;

public class HelloWorldGrain : IHelloWorldGrain, IGrainBase
{
    private readonly ILogger<HelloWorldGrain> _logger;

    public HelloWorldGrain(IGrainContext grainContext, ILogger<HelloWorldGrain> logger)
    {
        _logger = logger;
        GrainContext = grainContext;
    }

    public Task<HelloMessage> Hello(string name)
    {
        _logger.LogInformation("Received hello call from {Name}", name);

        return Task.FromResult(new HelloMessage($"Hello {name}, {this.GetPrimaryKeyString()} says hi! 👋", DateTimeOffset.UtcNow));
    }


    public Task<Foo> Foo()
    {

        var result = new Foo("Hello World");
        
        return Task.FromResult(result);
    }

    public Task<Bar> Bar()
    {
        return Task.FromResult((Bar)new Foo("Hello World"));
    }

    public Task<Baz> Baz()
    {
        return Task.FromResult((Baz)new Foo("Hello World"));
    }

    public Task<T?> FooBarOrBaz<T>() where T : Bar
    {
        var json = "{\"Message\":\"Hello World\"}";

        return Task.FromResult(JsonSerializer.Deserialize<T>(json));
    }
    
    public Task OnActivateAsync(CancellationToken token)
    {
        _logger.LogInformation("Hello grain {GrainId} was activated", GrainContext.GrainId);
        return Task.CompletedTask;
    }

    public IGrainContext GrainContext { get; }
}