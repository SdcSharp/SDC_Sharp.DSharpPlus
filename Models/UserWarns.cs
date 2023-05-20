#nullable enable
using DSharpPlus.Entities;
using SDC_Sharp.Types.Blacklist;

namespace SDC_Sharp.DSharpPlus.Models;

public sealed record UserWarns(ulong Id, sbyte Warns, string Type = "user", DiscordUser? User = null)
	: BaseUserWarns(Id, Warns, Type)
{
	public DiscordUser? User { get; internal set; } = User;
}