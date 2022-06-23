using Remora.Discord.API.Abstractions.Gateway.Events;
using Remora.Discord.Gateway;

namespace RemoraDSPEventsPOC;

public class DiscordClient
{
    private readonly DiscordGatewayClient _gateway;
    public DiscordClient(DiscordGatewayClient gateway) => _gateway = gateway;
    
    public async Task RunAsync() => _ = (await _gateway.RunAsync(CancellationToken.None)).Error is {} error ? throw new(error.Message) : default(object); 
    
    public event AsyncEventHandler<DiscordClient, IMessageCreate> MessageCreated 
    {
        add => _messageCreated.Register(value);
        remove => _messageCreated.Unregister(value);
    }
    
    internal readonly AsyncEvent<DiscordClient, IMessageCreate> _messageCreated = new();

    public event AsyncEventHandler<DiscordClient, IMessageDelete> MessageDeleted 
    {
        add => _messageDeleted.Register(value);
        remove => _messageDeleted.Unregister(value);
    }
    
    internal readonly AsyncEvent<DiscordClient, IMessageDelete> _messageDeleted = new();
    
    public event AsyncEventHandler<DiscordClient, IMessageUpdate> MessageUpdated 
    {
        add => _messageUpdated.Register(value);
        remove => _messageUpdated.Unregister(value);
    }
    
    internal readonly AsyncEvent<DiscordClient, IMessageUpdate> _messageUpdated = new();
}