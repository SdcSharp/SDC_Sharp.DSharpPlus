#nullable enable
using SDC_Sharp.SDC_Sharp.Types;

namespace SDC_Sharp.DSharpPlus
{
    public abstract class DiscordClientWrapperBase : IDiscordClientWrapper
    {
        public abstract int ShardCount { get; }
        public abstract int ServersCount { get; }
        public abstract ulong CurrentUserId { get; }
    }
}