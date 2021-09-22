using System;
using DSharpPlus;
using SDC_Sharp.DSharpPlus.Types;

namespace SDC_Sharp.DSharpPlus
{
    public class ShardedDiscordClientWrapper : IDiscordClientWrapper
    {
        private readonly DiscordClient _client;

        public ShardedDiscordClientWrapper(DiscordClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        DiscordClient IDiscordClientWrapper.Client => _client;
        
        public ulong CurrentUserId => _client.CurrentUser.Id;
        
        public int ServersCount => _client.Guilds.Count;
        
        public int ShardCount => _client.ShardCount;
    }
}