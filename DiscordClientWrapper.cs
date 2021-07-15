using System;
using DSharpPlus;

namespace SDC_Sharp.DSharpPlus
{
    public class DiscordClientWrapper : DiscordClientWrapperBase
    {
        private readonly DiscordClient _client;

        public DiscordClientWrapper(DiscordClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            SdcSharpExtensions.Discord = _client;
        }

        public override ulong CurrentUserId => _client.CurrentUser.Id;

        public override int ServersCount => _client.Guilds.Count;

        public override int ShardCount => 1;
    }
}