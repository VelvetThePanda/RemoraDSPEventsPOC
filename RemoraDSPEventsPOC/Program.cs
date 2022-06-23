using Microsoft.Extensions.DependencyInjection;
using Remora.Discord.Gateway.Extensions;
using RemoraDSPEventsPOC;

var services = new ServiceCollection().AddDiscordGateway(_ => "token_here").AddDSharpPlus();

var provider = services.BuildServiceProvider();

var client = provider.GetRequiredService<DiscordClient>();

client.MessageCreated += async (c, m) => Console.WriteLine(m);

