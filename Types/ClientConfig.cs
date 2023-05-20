using DSharpPlus;
using SDC_Sharp.DSharpPlus.Interfaces;

namespace SDC_Sharp.DSharpPlus.Types;

public class ClientConfig : IClientConfig
{
	private readonly DiscordClient m_client;

	public ClientConfig(DiscordClient client)
	{
		m_client = client;
		Rest = new Rest(client);
	}

	public uint ShardsCount => (uint)m_client.ShardCount;
	public uint GuildsCount => (uint)m_client.Guilds.Count;
	public IRest Rest { get; }
}