using Orleans;

namespace Serialization02.Grains.Interfaces;

[GenerateSerializer]
public record HelloMessage([property: Id(0)] string Message, [property: Id(1)] DateTimeOffset Timestamp);

[GenerateSerializer]
public record Foo([property: Id(1)] string Message) : Bar
{
    public override string Type => "Foo";
}
[GenerateSerializer]
public abstract record Bar : Baz;

[GenerateSerializer]
public abstract record Baz
{
    public abstract string Type { get; }
}