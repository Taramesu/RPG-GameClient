using System.Collections.Generic;
using RpgGame.Skill;
public class EntityTable
{
	public int Id;
	public string Name;
	public static List<EntityTable> Configs = new List<EntityTable>();

	public EntityTable() { }
	public EntityTable(int Id, string Name)
	{
		this.Id = Id;
		this.Name = Name;
	}
	public virtual EntityTable Clone()
	{
		var config = new EntityTable();
		this.MergeFrom(config);
		return config;
	}
	public virtual EntityTable MergeFrom(EntityTable source)
	{
		this.Id = source.Id;
		this.Name = source.Name;
		return this;
	}
	static EntityTable()
	{
		Configs = new List<EntityTable>
		{
			new EntityTable
			{
				Id = 0,
				Name = "player",
			},
			new EntityTable
			{
				Id = 1,
				Name = "monster1",
			},
			new EntityTable
			{
				Id = 2,
				Name = "monster2",
			},
		};
	}
	protected static Dictionary<int, EntityTable> TempDictById;
	public static EntityTable GetConfigById(int id)
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
