using Microsoft.Extensions.Hosting;
using Orleans.Configuration;
using Orleans.Hosting;

var builder = Host.CreateDefaultBuilder();

builder.UseOrleans((ctx, silo) =>
{
    silo.UseLocalhostClustering();
    silo.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Local";
        options.ServiceId = "Serialization02";
    });
});

var host = builder.Build();

await host.RunAsync();
