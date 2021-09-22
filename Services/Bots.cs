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
        private SdcSharpClient _sdcClient;
        internal Bots(ref SdcSharpClient client) => _sdcClient = client;

        public async Task<BotsResponse> UpdateStats(int serversCount, int shardsCount, ulong clientId)
        {
            await _sdcClient.RateLimiter();

            return await _sdcClient.PostRequest<BotsResponse>(
                $"bots/{(clientId != 0 ? clientId : _sdcClient.Wrapper.CurrentUserId)}/stats",
                new StringContent(JsonConvert.SerializeObject(new Dictionary<string, int>
                {
                    {
                        "shardsCount",
                        shardsCount != 0
                            ? shardsCount
                            : _sdcClient.Wrapper.ShardCount
                    },
                    {
                        "serversCount",
                        serversCount != 0
                            ? serversCount
                            : _sdcClient.Wrapper.ServersCount
                    }
                }), Encoding.UTF8, "application/json"));
        }

        public async Task AutoUpdateStats(int? serversCount, int? shardsCount, ulong? clientId,
            TimeSpan timeout = default)
        {
            timeout = timeout == default
                ? _sdcClient.DefaultTimeout
                : timeout >= TimeSpan.FromMinutes(30)
                    ? timeout
                    : TimeSpan.FromMinutes(30);

            while (true)
                try
                {
                    await UpdateStats(
                        serversCount ?? _sdcClient.Wrapper.ServersCount,
                        shardsCount ?? _sdcClient.Wrapper.ShardCount,
                        clientId ?? _sdcClient.Wrapper.CurrentUserId);
                    await Task.Delay(timeout);
                }
                catch
                {
                    return;
                }
        }
    }
}