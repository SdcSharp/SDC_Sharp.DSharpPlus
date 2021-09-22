using DSharpPlus;

namespace SDC_Sharp.DSharpPlus.Types
{
    public interface IDiscordClientWrapper : SDC_Sharp.Types.IDiscordClientWrapper
    {
        internal DiscordClient Client { get; }
    }
}