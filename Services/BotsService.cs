using System;
using System.Threading;
using System.Threading.Tasks;
using SDC_Sharp.DSharpPlus.Interfaces;
using SDC_Sharp.Services;
using SDC_Sharp.Types.Bots;

namespace SDC_Sharp.DSharpPlus.Services;

public sealed class BotsService : BaseBotsService
{
	private readonly IClientConfig m_clientConfig;

	public BotsService(ISdcSharpClient client, ISdcServices sdcServices) : base(client)
	{
		m_clientConfig = sdcServices.Client;
	}

	public void AutoPostStats(TimeSpan interval,
		bool logging = false,
		CancellationToken cancellationToken = default)
	{
		AutoPostStats(
			interval,
			m_clientConfig.Rest.CurrentUserId,
			m_clientConfig.ShardsCount,
			m_clientConfig.GuildsCount,
			logging,
			cancellationToken);
	}

	public Task<StatsResponse> PostStats()
	{
		return PostStats<StatsResponse>(
			m_clientConfig.Rest.CurrentUserId,
			m_clientConfig.ShardsCount,
			m_clientConfig.GuildsCount);
	}
}