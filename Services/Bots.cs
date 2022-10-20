using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SDC_Sharp.DSharpPlus.Types;
using SDC_Sharp.SDC_Sharp;

namespace SDC_Sharp.DSharpPlus.Services
{
    public class Bots
    {
        private SdcSharpClient m_sdcClient;
        internal Bots(ref SdcSharpClient client) => m_sdcClient = client;

        public async Task<BotsResponse> UpdateStats(int serversCount, int shardsCount, ulong clientId)
        {
            await m_sdcClient.RateLimiter();

            return await m_sdcClient.PostRequest<BotsResponse>(
                $"bots/{(clientId != 0 ? clientId : m_sdcClient.Wrapper.CurrentUserId)}/stats",
                new StringContent(JsonConvert.SerializeObject(new Dictionary<string, int>
                {
                    {
                        "shardsCount",
                        shardsCount != 0
                            ? shardsCount
                            : m_sdcClient.Wrapper.ShardCount
                    },
                    {
                        "serversCount",
                        serversCount != 0
                            ? serversCount
                            : m_sdcClient.Wrapper.ServersCount
                    }
                }), Encoding.UTF8, "application/json"));
        }

        public async Task AutoUpdateStats(int? serversCount, int? shardsCount, ulong? clientId,
            TimeSpan timeout = default)
        {
            timeout = timeout == default
                ? m_sdcClient.DefaultTimeout
                : timeout >= TimeSpan.FromMinutes(30)
                    ? timeout
                    : TimeSpan.FromMinutes(30);

            while (true)
                try
                {
                    await UpdateStats(
                        serversCount ?? m_sdcClient.Wrapper.ServersCount,
                        shardsCount ?? m_sdcClient.Wrapper.ShardCount,
                        clientId ?? m_sdcClient.Wrapper.CurrentUserId);
                    await Task.Delay(timeout);
                }
                catch
                {
                    return;
                }
        }
    }
}