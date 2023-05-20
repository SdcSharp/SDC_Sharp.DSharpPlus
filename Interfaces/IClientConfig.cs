namespace SDC_Sharp.DSharpPlus.Interfaces;

public interface IClientConfig
{
	public uint ShardsCount { get; }
	public uint GuildsCount { get; }
	public IRest Rest { get; }
}