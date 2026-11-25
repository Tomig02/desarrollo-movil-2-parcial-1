namespace parcial_1_tests.Testing;

internal sealed class StubServiceProvider : IServiceProvider
{
    public object? GetService(Type serviceType) => null;
}
