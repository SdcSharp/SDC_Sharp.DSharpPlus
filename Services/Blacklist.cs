using System.Threading.Tasks;
using SDC_Sharp.DSharpPlus.Types;
using static SDC_Sharp.SdcSharpExtensions;

namespace SDC_Sharp.DSharpPlus.Services
{
    public class Blacklist
    {
        internal Blacklist()
        {
        }

        public async Task<BlacklistResponse> GetWarns(ulong id)
        {
            var res = await SdcClient.GetRequest<BlacklistResponse>($"warns/{id}");
            if (res.id == 0)
                res.id = id;

            return res;
        }
    }
}