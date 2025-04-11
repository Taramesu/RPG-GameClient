using System.Collections.Generic;
public class UnitTable
{
	public int Id;
	public string Name;
	public float Hp;
	public float ATK;
	public float Speed;
	public static List<UnitTable> Configs = new List<UnitTable>();

	public UnitTable() { }
	public UnitTable(int Id, string Name, float Hp, float ATK, float Speed)
	{
		this.Id = Id;
		this.Name = Name;
		this.Hp = Hp;
		this.ATK = ATK;
		this.Speed = Speed;
	}
	public virtual UnitTable Clone()
	{
		var config = new UnitTable();
		this.MergeFrom(config);
		return config;
	}
	public virtual UnitTable MergeFrom(UnitTable source)
	{
		this.Id = source.Id;
		this.Name = source.Name;
		this.Hp = source.Hp;
		this.ATK = source.ATK;
		this.Speed = source.Speed;
		return this;
	}
	static UnitTable()
	{
		Configs = new List<UnitTable>
		{
			new UnitTable
			{
				Id = 0,
				Name = "Jerry",
				Hp = 100f,
				ATK = 20f,
				Speed = 300f,
			},
			new UnitTable
			{
				Id = 1,
				Name = "Tom",
				Hp = 300f,
				ATK = 40f,
				Speed = 275f,
			},
		};
	}
	protected static Dictionary<int, UnitTable> TempDictById;
	public static UnitTable GetConfigById(int id)
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
