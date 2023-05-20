#nullable enable
using DSharpPlus.Entities;
using SDC_Sharp.Types.Monitoring;

namespace SDC_Sharp.DSharpPlus.Models;

public sealed class Guild : BaseGuild
{
	public ulong Id { get; set; }
	public string Url { get; set; } = default!;
	public DiscordGuild? Instance { get; set; }
}