using System;
using System.Linq;
using DSharpPlus;

namespace SDC_Sharp.DSharpPlus
{
    public class ShardedDiscordClientWrapper : DiscordClientWrapperBase
    {
        private readonly DiscordClient _client;
        private readonly DiscordShardedClient _shards;

        public ShardedDiscordClientWrapper(DiscordShardedClient client)
        {
            _shards = client;
            _client = client.ShardClients.Values.First() ?? throw new ArgumentNullException(nameof(client));
            SdcSharpExtensions.Discord = _client;
        }

        public override ulong CurrentUserId => _client.CurrentUser.Id;
        public override int ServersCount => _client.Guilds.Count;
        public override int ShardCount => _shards.ShardClients.Count;
    }
}