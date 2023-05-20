using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using SDC_Sharp.DSharpPlus.Interfaces;

namespace SDC_Sharp.DSharpPlus.Types;

public class Rest : IRest
{
	private readonly DiscordClient m_client;

	public Rest(DiscordClient client)
	{
		m_client = client;
	}

	public ulong CurrentUserId => m_client.CurrentUser.Id;

	public Task<DiscordUser> GetUserAsync(ulong userId)
	{
		return m_client.GetUserAsync(userId);
	}

	public Task<DiscordGuild> GetGuildAsync(ulong guildId)
	{
		return m_client.GetGuildAsync(guildId);
	}
}