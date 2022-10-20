using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SDC_Sharp.DSharpPlus.Types;
using SDC_Sharp.SDC_Sharp;

namespace SDC_Sharp.DSharpPlus.Services
{
    public class Monitoring
    {
        private SdcSharpClient m_sdcClient;
        internal Monitoring(ref SdcSharpClient client) => m_sdcClient = client;

        public async Task<GuildInfo> GetGuild(ulong guildId)
        {
            var res = await m_sdcClient.GetRequest<GuildInfo>($"guild/{guildId}");
            res.Id = guildId;

            return res;
        }

        public async Task<GuildPlace> GetGuildPlace(ulong guildId)
        {
            var res = await m_sdcClient.GetRequest<GuildPlace>($"guild/{guildId}/place");
            res.Id = guildId;

            return res;
        }

        public async Task<UserRatedServers> GetUserRatedServers(ulong userId, bool fetch = false)
        {
            var raw = await m_sdcClient.GetRequest<Dictionary<string, byte>>($"user/{userId}/rated");
            var list = new LinkedList<UserRate>();

            await Task.Run(async () =>
            {
                foreach (var (id, rate) in raw)
                {
                    list.AddLast(new UserRate
                    {
                        Id = ulong.Parse(id),
                        Rate = rate,
                        Guild = !fetch ? null : await m_sdcClient.Wrapper.GetGuild(ulong.Parse(id))
                    });
                }
            });

            return new UserRatedServers
            {
                RatedServersList = list.ToArray()
            };
        }

        public async Task<GuildRatedUsers> GetGuildRated(ulong guildId, bool fetch = false)
        {
            var raw = await m_sdcClient.GetRequest<Dictionary<string, byte>>($"guild/{guildId}/rated");
            var list = new LinkedList<UserRate>();

            await Task.Run(async () =>
            {
                foreach (var (id, rate) in raw)
                {
                    list.AddLast(new UserRate
                    {
                        Id = ulong.Parse(id),
                        Rate = rate,
                        User = !fetch ? null : await m_sdcClient.Wrapper.GetUser(ulong.Parse(id))
                    });
                }
            });

            return new GuildRatedUsers
            {
                RatedUsersList = list.ToArray()
            };
        }
    }
}