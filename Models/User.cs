#nullable enable
using DSharpPlus.Entities;

namespace SDC_Sharp.DSharpPlus.Models;

public record struct User(ulong Id, DiscordUser? Instance = null)
{
	public DiscordUser? Instance { get; init; } = Instance;
	public ulong Id { get; init; } = Id;
}