using System.Threading.Tasks;
using SDC_Sharp.DSharpPlus.Types;
using SDC_Sharp.SDC_Sharp;

namespace SDC_Sharp.DSharpPlus.Services
{
    public class Blacklist
    {
        private SdcSharpClient _sdcClient;
        internal Blacklist(ref SdcSharpClient client) => _sdcClient = client;

        public async Task<BlacklistResponse> GetWarns(ulong id)
        {
            var res = await _sdcClient.GetRequest<BlacklistResponse>($"warns/{id}");
            res.SdcClient = _sdcClient;
            if (res.Id == 0)
                res.Id = id;

            return res;
        }
    }
}
