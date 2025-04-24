using System.Collections.Generic;
using RpgGame.Skill;
public class MonsterLevelTable
{
	public int Id;
	public int MinLevel;
	public int MaxLevel;
	public int Exp;
	public static List<MonsterLevelTable> Configs = new List<MonsterLevelTable>();

	public MonsterLevelTable() { }
	public MonsterLevelTable(int Id, int MinLevel, int MaxLevel, int Exp)
	{
		this.Id = Id;
		this.MinLevel = MinLevel;
		this.MaxLevel = MaxLevel;
		this.Exp = Exp;
	}
	public virtual MonsterLevelTable Clone()
	{
		var config = new MonsterLevelTable();
		this.MergeFrom(config);
		return config;
	}
	public virtual MonsterLevelTable MergeFrom(MonsterLevelTable source)
	{
		this.Id = source.Id;
		this.MinLevel = source.MinLevel;
		this.MaxLevel = source.MaxLevel;
		this.Exp = source.Exp;
		return this;
	}
	static MonsterLevelTable()
	{
		Configs = new List<MonsterLevelTable>
		{
			new MonsterLevelTable
			{
				Id = 1,
				MinLevel = 1,
				MaxLevel = 30,
				Exp = 50,
			},
			new MonsterLevelTable
			{
				Id = 2,
				MinLevel = 1,
				MaxLevel = 20,
				Exp = 30,
			},
		};
	}
	protected static Dictionary<int, MonsterLevelTable> TempDictById;
	public static MonsterLevelTable GetConfigById(int id)
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
