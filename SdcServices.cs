using DSharpPlus;
using SDC_Sharp.DSharpPlus.Interfaces;
using SDC_Sharp.DSharpPlus.Types;

namespace SDC_Sharp.DSharpPlus;

public sealed class SdcServices : ISdcServices
{
	public IClientConfig Client { get; }

	public SdcServices(IClientConfig config)
	{
		Client = config;
	}

	public SdcServices(DiscordClient client)
	{
		Client = new ClientConfig(client);
	}
}