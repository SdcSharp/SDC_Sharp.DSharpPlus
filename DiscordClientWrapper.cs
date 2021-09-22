using System;
using DSharpPlus;
using SDC_Sharp.DSharpPlus.Types;

namespace SDC_Sharp.DSharpPlus
{
    public class DiscordClientWrapper : IDiscordClientWrapper
    {
        internal readonly DiscordClient _client;

        public DiscordClientWrapper(DiscordClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        DiscordClient IDiscordClientWrapper.Client => _client;

        public ulong CurrentUserId => _client.CurrentUser.Id;

        public int ServersCount => _client.Guilds.Count;

        public int ShardCount => 1;
    }
}