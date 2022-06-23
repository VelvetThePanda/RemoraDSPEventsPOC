using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Remora.Discord.Gateway.Extensions;
using Remora.Results;

namespace RemoraDSPEventsPOC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDSharpPlus(this IServiceCollection services)
    {
        services.TryAddSingleton<DiscordClient>();
        services.AddResponder<DiscordClientResponder>();
        
        return services;
    }
}