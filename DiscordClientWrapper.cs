using System;
using DSharpPlus;
using SDC_Sharp.DSharpPlus.Types;

namespace SDC_Sharp.DSharpPlus
{
    public class DiscordClientWrapper : IDiscordClientWrapper
    {
        internal readonly DiscordClient Client;

        public DiscordClientWrapper(DiscordClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        DiscordClient IDiscordClientWrapper.Client => Client;

        public ulong CurrentUserId => Client.CurrentUser.Id;

        public int ServersCount => Client.Guilds.Count;

        public int ShardCount => 1;
    }
}