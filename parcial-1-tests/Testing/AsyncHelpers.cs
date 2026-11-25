namespace parcial_1_tests.Testing;

internal static class AsyncHelpers
{
    public static async Task<bool> WaitForAsync(Func<bool> condition)
    {
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(3));

        try
        {
            while (!condition())
                await Task.Delay(10, timeout.Token);

            return true;
        }
        catch (OperationCanceledException)
        {
            return condition();
        }
    }
}
