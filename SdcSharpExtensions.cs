using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.Extensions.DependencyInjection;
using SDC_Sharp.DSharpPlus.Services;

namespace SDC_Sharp
{
    public static class SdcSharpExtensions
    {
        internal static SdcSharpClient SdcClient;
        internal static DiscordClient Discord;


        public static IServiceCollection AddSdcClient(this IServiceCollection serviceCollection,
            SdcSharpClient sdcSharpClient)
        {
            SdcClient = sdcSharpClient;

            serviceCollection.AddSingleton(sdcSharpClient);
            return serviceCollection;
        }

        public static async Task<DiscordUser> GetUser(this IDiscordClientWrapper client, ulong id)
        {
            return await Discord.GetUserAsync(id);
        }

        public static async Task<DiscordGuild> GetGuild(this IDiscordClientWrapper client, ulong id)
        {
            return await Discord.GetGuildAsync(id);
        }

        public static Bots GetBots(this SdcSharpClient client)
        {
            return new Bots();
        }

        public static Monitoring GetMonitoring(this SdcSharpClient client)
        {
            return new Monitoring();
        }

        public static Blacklist GetBlacklist(this SdcSharpClient client)
        {
            return new Blacklist();
        }
    }
}