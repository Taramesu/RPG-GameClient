using System.Collections.Generic;
public class ItemTable
{
	public int Id;
	public string Name;
	public string Element;
	public string Desc;
	public static List<ItemTable> Configs = new List<ItemTable>();

	public ItemTable() { }
	public ItemTable(int Id, string Name, string Element, string Desc)
	{
		this.Id = Id;
		this.Name = Name;
		this.Element = Element;
		this.Desc = Desc;
	}
	public virtual ItemTable Clone()
	{
		var config = new ItemTable();
		this.MergeFrom(config);
		return config;
	}
	public virtual ItemTable MergeFrom(ItemTable source)
	{
		this.Id = source.Id;
		this.Name = source.Name;
		this.Element = source.Element;
		this.Desc = source.Desc;
		return this;
	}
	static ItemTable()
	{
		Configs = new List<ItemTable>
		{
			new ItemTable
			{
				Id = 35001,
				Name = "火灵珠",
				Element = "火",
				Desc = "发射火焰",
			},
			new ItemTable
			{
				Id = 35002,
				Name = "水灵珠",
				Element = "水",
				Desc = "释放洪水",
			},
			new ItemTable
			{
				Id = 35003,
				Name = "土灵珠",
				Element = "土",
				Desc = "升起土墙",
			},
			new ItemTable
			{
				Id = 35004,
				Name = "木灵珠",
				Element = "木",
				Desc = "植被生长",
			},
			new ItemTable
			{
				Id = 35005,
				Name = "金灵珠",
				Element = "金",
				Desc = "操控金属",
			},
		};
	}
	protected static Dictionary<int, ItemTable> TempDictById;
	public static ItemTable GetConfigById(int id)
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
