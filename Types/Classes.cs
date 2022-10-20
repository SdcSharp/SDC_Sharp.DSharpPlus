using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using SDC_Sharp.SDC_Sharp;
using static SDC_Sharp.DSharpPlus.SdcSharpExtensions;

namespace SDC_Sharp.DSharpPlus.Types
{
    public class BotResponseError
    {
        public string Msg { get; set; }
        public string Type { get; set; }
        public int Code { get; set; }

        public DiscordEmbed ToEmbed()
        {
            var embed = new DiscordEmbedBuilder();
            
            embed.AddField("Type: ", $"`{Type}`", true);
            embed.AddField("Code: ", $"`{Code}`", true);
            embed.AddField("Message: ", $"`{Msg}`", true);

            return embed.Build();
        }

        public override string ToString()
        {
            return $"{GetType()}: " +
                   "{ " +
                   $"Type: {Type}, " +
                   $"Code: {Code.ToString()}, " +
                   $"Message: {Msg}" +
                   " }";
        }
    }
    
    public struct BotsResponse
    {
        public bool Status { get; set; }
        public BotResponseError Error { get; set; }

        public override string ToString()
        {
            return
                $"{GetType()}: " + (Error == null
                    ? "{ " +
                      $"status: {(!Status ? "null" : Status.ToString())}, " +
                      " }"
                    : Error.ToString());
        }
    }

    public struct BlacklistResponse
    {
        internal SdcSharpClient SdcClient;
        
        public BotResponseError Error { get; set; }
        public ulong Id { get; set; }
        public string Type { get; set; }
        public sbyte Warns { get; set; }

        public override string ToString()
        {
            return $"{GetType()}: " + (Error == null
                ? "{ " +
                  $"id: {Id.ToString()}, " +
                  $"type: {Type}, " +
                  $"warns: {Warns.ToString()}" +
                  " }"
                : Error.ToString());
        }

        public async Task<DiscordEmbed> ToEmbed()
        {
            var embed = new DiscordEmbedBuilder();
            embed.Title = "Варны";
            embed.Description = "\n";

            if (Id != 0)
                try
                {
                    await SdcClient.RateLimiter();
                    embed.Title += " пользователя";
                    embed.Description += $"{await SdcClient.Wrapper.GetUser(Id)}\n";
                }
                catch
                {
                }

            embed.Description += Warns;

            return embed.Build();
        }
    }

    public struct UserRatedServers
    {
        public BotResponseError Error { get; set; }
        public UserRate[] RatedServersList { get; set; }
        
        public override string ToString()
        {
            return $"{GetType()}: " +
                   (Error == null
                       ? "{ " +
                         (RatedServersList != null && RatedServersList.Length != 0
                             ? (RatedServersList.Length > 1
                                 ? $"[{string.Join(", ", RatedServersList.Select(x => x.ToString()))}]"
                                 : $"[{RatedServersList.First()}]")
                             : "[]") +
                         " }"
                       : Error.ToString());
        }
    }

    public struct UserRate
    {
        public ulong Id { get; set; }
        public byte Rate { get; set; }

        public DiscordUser User { get; set; }
        public DiscordGuild Guild { get; set; }

        public override string ToString()
        {
            return $"\"{Id}\": {Rate}";
        }
    }

    public struct GuildPlace
    {
        public BotResponseError Error { get; set; }

        public ulong Id { get; set; }
        public uint Place { get; set; }
        
        public override string ToString()
        {
            return $"{GetType()}: " +
                   (Error == null
                       ? "{ " +
                         $"place: {Place}" +
                         " }"
                       : Error.ToString());
        }
    }

    public struct GuildRatedUsers
    {
        public BotResponseError Error { get; set; }
        public UserRate[] RatedUsersList { get; set; }
        
        public override string ToString()
        {
            return $"{GetType()}: " +
                   (Error == null
                       ? "{ " +
                         (RatedUsersList != null && RatedUsersList.Length != 0
                             ? (RatedUsersList.Length > 1
                                 ? $"[{string.Join(", ", RatedUsersList.Select(x => x.ToString()))}]"
                                 : $"[{RatedUsersList.First()}]")
                             : "[]") +
                         " }"
                       : Error.ToString());
        }
    }

    public struct GuildInfo
    {
        public BotResponseError Error { get; set; }

        public string Avatar { get; set; }
        public string Lang { get; set; }
        public string Name { get; set; }
        public string Des { get; set; }
        public string Invite { get; set; }
        public string Owner { get; set; }
        public string Tags { get; set; }

        public ulong Id { get; set; }
        public ulong UpCount { get; set; }

        public uint Online { get; set; }
        public uint Members { get; set; }

        public byte Bot { get; set; }

        public BoostLevelEnum Boost { get; set; }
        public BadgesEnum Status { get; set; }

        private BadgesEnum[] m_bages;
        public BadgesEnum[] Bages
        {
            get
            {
                if (m_bages == null || m_bages.Length < 1)
                    m_bages = GetBadgesEnums(Status).GetAwaiter().GetResult();

                return m_bages;
            }

            set
            {
                if (value.Length >= 1)
                    m_bages = value;
                else
                    m_bages = Array.Empty<BadgesEnum>();
            }
        }

        private static Task<BadgesEnum[]> GetBadgesEnums(BadgesEnum bage)
        {
            var res = new LinkedList<BadgesEnum>();

            return Task.Run(() =>
            {
                for (var i = 1; i <= 0x200; i *= 2)
                {
                    if (Enum.TryParse<BadgesEnum>(i.ToString(), out var status) && (bage & status) == status)
                        res.AddLast(bage);
                }
                
                return res.ToArray();
            });
        }

        public DiscordEmbedBuilder ToEmbed()
        {
            var embed = new DiscordEmbedBuilder();

            embed.Description = Des;
            embed.Author = new DiscordEmbedBuilder.EmbedAuthor()
            {
                Name = Name,
                Url = $"https://server-discord.com/{Id}",
                IconUrl = $"https://cdn.discordapp.com/icons/{Id}/{Avatar}.png"
            };
            embed.Footer = new DiscordEmbedBuilder.EmbedFooter()
            {
                Text = Owner
            };

            embed.AddField("Ап-очки: ", $"[`{UpCount}`](https://server-discord.com/faq)");
            embed.AddField("Уровень буста: ", $"[`BOOST {Boost.ToString()}`](https://server-discord.com/boost)");
            embed.AddField("Бейдж: ", $"[`{(Bages.Length != 0 ? (Bages.Length > 1 ? string.Join(", ", Bages) : Bages.First().ToString()) : "нет")}`](https://server-discord.com/faq)");
            embed.AddField("Теги:",
                Tags.Length != 0 && Tags.Split(",").Length >= 2 ? $"`{string.Join("`, `", Tags.Split(","))}`" :
                Tags.Length != 0 ? $"`{Tags}`" : "не указаны");
            embed.AddField("Онлайн:", $"`{Online}`", true);
            return embed;
        }

        public override string ToString()
        {
            return $"{GetType()}: " + (Error == null
                ? "{ " +
                  $"avatar: \"{Avatar}\", " +
                  $"lang: \"{Lang}\", " +
                  $"name: \"{Name}\", " +
                  $"des: \"{Des}\", " +
                  $"invite: \"{Invite}\", " +
                  $"owner: \"{Owner}\", " +
                  $"tags: {Tags}, " +
                  $"id: {Id.ToString()}, " +
                  $"upCount: {UpCount.ToString()}, " +
                  $"online: {Online.ToString()}, " +
                  $"members: {Members.ToString()}, " +
                  $"bot: {Bot.ToString()}, " +
                  $"boost: {((int) Boost).ToString()}, " +
                  $"status: {((int) Status).ToString()}, " +
                  " }"
                : Error.ToString());
        }
    }
    
    [Flags]
    public enum BoostLevelEnum
    {
        None = 0,
        Light = 1,
        Pro = 2,
        Max = 3
    }

    [Flags]
    public enum BadgesEnum
    {
        Sitedev = 0x1,
        Verefied = 0x2,
        Partner = 0x4,
        Favorite = 0x8,
        Bughunter = 0x10,
        Easteregg = 0x20,
        Botdev = 0x40,
        Youtube = 0x80,
        Twitch = 0x100,
        Spamhunt = 0x200
    }
}