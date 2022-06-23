using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace RemoraDSPEventsPOC;

public delegate Task AsyncEventHandler<in TSender, in TEventArgs>(TSender sender, TEventArgs args);

public class AsyncEvent<TSender, TEventArgs>
{
    private readonly List<AsyncEventHandler<TSender, TEventArgs>> _handlers = new();
    
    private readonly SemaphoreSlim _sync = new(1);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Register(AsyncEventHandler<TSender, TEventArgs> handler) => _handlers.Add(handler);
    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Unregister(AsyncEventHandler<TSender, TEventArgs> handler) => _handlers.Remove(handler);
    
    public async Task InvokeAsync(TSender sender, TEventArgs args, Func<Exception, Task> onException)
    {
        await _sync.WaitAsync();

        var exceptions = new List<Exception>();
        
        foreach (var handler in _handlers)
        {
            try
            {
                await handler(sender, args);
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }
        }
        _sync.Release();
        
        if (exceptions.Count > 0)
        {
            await onException(new AggregateException(exceptions));
        }
    }
}