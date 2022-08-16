using Orleans;

namespace Serialization02.Grains.Interfaces
{
    public interface IHelloWorldGrain : IGrainWithStringKey
    {
        public const string Key = "HelloWorld";

        Task<HelloMessage> Hello(string name);

        Task<Foo> Foo();
        Task<Bar> Bar();
        Task<Baz> Baz();

        Task<T?> FooBarOrBaz<T>() where T : Bar;
    }
}