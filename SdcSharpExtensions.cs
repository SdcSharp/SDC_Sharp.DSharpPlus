using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.Extensions.DependencyInjection;
using SDC_Sharp.DSharpPlus.Services;
using SDC_Sharp.DSharpPlus.Types;
using SDC_Sharp.SDC_Sharp;
using IDiscordClientWrapper = SDC_Sharp.SDC_Sharp.Types.IDiscordClientWrapper;

namespace SDC_Sharp.DSharpPlus
{
    public static class SdcSharpExtensions
    {
        private static SdcSharpClient m_sdcClient;
        
        private static Bots m_bots;
        private static Monitoring m_monitoring;
        private static Blacklist m_blacklist;
        
        private static DiscordClient m_discord;
        
        public static Bots GetBots(this SdcSharpClient client) => m_bots;
        public static Monitoring GetMonitoring(this SdcSharpClient client) => m_monitoring;
        public static Blacklist GetBlacklist(this SdcSharpClient client) => m_blacklist;
        
        public static IServiceCollection AddSdcClient(this IServiceCollection serviceCollection,
            SdcClientConfig config)
        {
            m_discord = config.wrapper.Client;
            m_sdcClient = new SdcSharpClient(config);

            m_bots = new Bots(ref m_sdcClient);
            m_monitoring = new Monitoring(ref m_sdcClient);
            m_blacklist = new Blacklist(ref m_sdcClient);

            serviceCollection.AddSingleton(m_sdcClient);
            return serviceCollection;
        }

        public static async Task<DiscordUser> GetUser(this IDiscordClientWrapper client, ulong id)
        {
            return await m_discord.GetUserAsync(id);
        }

        public static async Task<DiscordGuild> GetGuild(this IDiscordClientWrapper client, ulong id)
        {
            return await m_discord.GetGuildAsync(id);
        }
    }
}