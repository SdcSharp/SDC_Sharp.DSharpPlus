using System;
using DSharpPlus;
using SDC_Sharp.DSharpPlus.Types;

namespace SDC_Sharp.DSharpPlus
{
    public class ShardedDiscordClientWrapper : IDiscordClientWrapper
    {
        private readonly DiscordClient m_client;

        public ShardedDiscordClientWrapper(DiscordClient client)
        {
            m_client = client ?? throw new ArgumentNullException(nameof(client));
        }

        DiscordClient IDiscordClientWrapper.Client => m_client;
        
        public ulong CurrentUserId => m_client.CurrentUser.Id;
        
        public int ServersCount => m_client.Guilds.Count;
        
        public int ShardCount => m_client.ShardCount;
    }
}