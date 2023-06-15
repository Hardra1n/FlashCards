namespace Common.RpcClient;

public class TimeoutToken : IDisposable
{
    private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    public CancellationToken Token { private set; get; }

    public TimeoutToken(int millisecondsDelay)
    {
        cancellationTokenSource.CancelAfter(millisecondsDelay);
        Token = cancellationTokenSource.Token;
    }

    public void Dispose()
    {
        cancellationTokenSource.Dispose();
    }
}