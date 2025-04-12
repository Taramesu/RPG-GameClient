using System.Collections.Generic;
public class PlayerLevelTable
{
	public int Id;
	public int MaxLevel;
	public int InitLevelExp;
	public int ExpGrowth;
	public static List<PlayerLevelTable> Configs = new List<PlayerLevelTable>();

	public PlayerLevelTable() { }
	public PlayerLevelTable(int Id, int MaxLevel, int InitLevelExp, int ExpGrowth)
	{
		this.Id = Id;
		this.MaxLevel = MaxLevel;
		this.InitLevelExp = InitLevelExp;
		this.ExpGrowth = ExpGrowth;
	}
	public virtual PlayerLevelTable Clone()
	{
		var config = new PlayerLevelTable();
		this.MergeFrom(config);
		return config;
	}
	public virtual PlayerLevelTable MergeFrom(PlayerLevelTable source)
	{
		this.Id = source.Id;
		this.MaxLevel = source.MaxLevel;
		this.InitLevelExp = source.InitLevelExp;
		this.ExpGrowth = source.ExpGrowth;
		return this;
	}
	static PlayerLevelTable()
	{
		Configs = new List<PlayerLevelTable>
		{
			new PlayerLevelTable
			{
				Id = 0,
				MaxLevel = 100,
				InitLevelExp = 500,
				ExpGrowth = 200,
			},
		};
	}
	protected static Dictionary<int, PlayerLevelTable> TempDictById;
	public static PlayerLevelTable GetConfigById(int id)
	{
		if (TempDictById == null)
		{
			TempDictById = new(Configs.Count);
			for(var i = 0; i < Configs.Count; i++)
			{
				var c = Configs[i];
				TempDictById.Add(c.Id, c);
			}
		}
#if UNITY_EDITOR
		if (TempDictById.Count != Configs.Count)
			UnityEngine.Debug.LogError($"配表数据不一致(ConfigsUnmatched): {TempDictById.Count}!={Configs.Count}");
#endif
		return TempDictById.GetValueOrDefault(id);
	}
}
