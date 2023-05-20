#nullable enable
using DSharpPlus.Entities;

namespace SDC_Sharp.DSharpPlus.Models;

public record struct RatedGuild(ulong Id, DiscordGuild? Instance = null)
{
	public ulong Id { get; init; } = Id;
	public DiscordGuild? Instance { get; init; } = Instance;
}