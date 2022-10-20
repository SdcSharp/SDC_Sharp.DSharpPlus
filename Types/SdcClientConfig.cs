using SDC_Sharp.SDC_Sharp.Types;

namespace SDC_Sharp.DSharpPlus.Types
{
    public class SdcClientConfig : ISdcClientConfig
    {
        private string m_token;
        internal IDiscordClientWrapper wrapper;

        public SDC_Sharp.Types.IDiscordClientWrapper Wrapper
        {
            get => wrapper;
            set => wrapper = value as IDiscordClientWrapper;
        }

        public virtual string Token
        {
            get => m_token;
            set => m_token = value.StartsWith("SDC ") ? value : $"SDC {value}";
        }
    }
}