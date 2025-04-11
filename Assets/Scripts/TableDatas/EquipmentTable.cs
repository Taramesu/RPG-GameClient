using System.Collections.Generic;
public class EquipmentTable
{
	public int Id;
	public string Name;
	public float Rare;
	public float BaseATK;
	public float BaseMaxATK;
	public string Desc;
	public static List<EquipmentTable> Configs = new List<EquipmentTable>();

	public EquipmentTable() { }
	public EquipmentTable(int Id, string Name, float Rare, float BaseATK, float BaseMaxATK, string Desc)
	{
		this.Id = Id;
		this.Name = Name;
		this.Rare = Rare;
		this.BaseATK = BaseATK;
		this.BaseMaxATK = BaseMaxATK;
		this.Desc = Desc;
	}
	public virtual EquipmentTable Clone()
	{
		var config = new EquipmentTable();
		this.MergeFrom(config);
		return config;
	}
	public virtual EquipmentTable MergeFrom(EquipmentTable source)
	{
		this.Id = source.Id;
		this.Name = source.Name;
		this.Rare = source.Rare;
		this.BaseATK = source.BaseATK;
		this.BaseMaxATK = source.BaseMaxATK;
		this.Desc = source.Desc;
		return this;
	}
	static EquipmentTable()
	{
		Configs = new List<EquipmentTable>
		{
			new EquipmentTable
			{
				Id = 0,
				Name = "sword",
				Rare = 0.567f,
				BaseATK = 20f,
				BaseMaxATK = 199f,
				Desc = "高攻击",
			},
			new EquipmentTable
			{
				Id = 1,
				Name = "knife",
				Rare = 0.996f,
				BaseATK = 10f,
				BaseMaxATK = 99f,
				Desc = "低攻击",
			},
		};
	}
	protected static Dictionary<int, EquipmentTable> TempDictById;
	public static EquipmentTable GetConfigById(int id)
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
