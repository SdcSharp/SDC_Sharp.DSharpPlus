using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace SDC_Sharp.DSharpPlus.Interfaces;

public interface IRest
{
	public ulong CurrentUserId { get; }
	public Task<DiscordUser> GetUserAsync(ulong userId);
	public Task<DiscordGuild> GetGuildAsync(ulong guildId);
}