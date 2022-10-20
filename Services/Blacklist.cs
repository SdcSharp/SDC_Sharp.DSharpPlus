using System.Threading.Tasks;
using SDC_Sharp.DSharpPlus.Types;
using SDC_Sharp.SDC_Sharp;

namespace SDC_Sharp.DSharpPlus.Services
{
    public class Blacklist
    {
        private SdcSharpClient m_sdcClient;
        internal Blacklist(ref SdcSharpClient client) => m_sdcClient = client;

        public async Task<BlacklistResponse> GetWarns(ulong id)
        {
            var res = await m_sdcClient.GetRequest<BlacklistResponse>($"warns/{id}");
            res.SdcClient = m_sdcClient;
            if (res.Id == 0)
                res.Id = id;

            return res;
        }
    }
}
