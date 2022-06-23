using Remora.Discord.API.Abstractions.Gateway.Events;
using Remora.Discord.Gateway.Responders;
using Remora.Results;

namespace RemoraDSPEventsPOC;

public class DiscordClientResponder : IResponder<IMessageCreate>, IResponder<IMessageUpdate>, IResponder<IMessageDelete>
{
    private readonly DiscordClient _client;
    public DiscordClientResponder(DiscordClient client) => _client = client;

    public async Task<Result> RespondAsync(IMessageCreate gatewayEvent, CancellationToken ct = default)
    {
        await _client._messageCreated.InvokeAsync(_client, gatewayEvent, _ => Task.CompletedTask);
        return Result.FromSuccess();
    }

    public async Task<Result> RespondAsync(IMessageUpdate gatewayEvent, CancellationToken ct = default)
    {
        await _client._messageUpdated.InvokeAsync(_client, gatewayEvent, _ => Task.CompletedTask);
        return Result.FromSuccess();
    }

    public async Task<Result> RespondAsync(IMessageDelete gatewayEvent, CancellationToken ct = default)
    {
        await _client._messageDeleted.InvokeAsync(_client, gatewayEvent, _ => Task.CompletedTask);
        return Result.FromSuccess();
    }
}