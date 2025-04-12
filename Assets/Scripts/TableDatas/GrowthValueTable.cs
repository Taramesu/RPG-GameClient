using System.Collections.Generic;
public class GrowthValueTable
{
	public int Id;
	public float HpGrowth;
	public float MpGrowth;
	public float ATKGrowth;
	public float DEFGrowth;
	public float CritGrowth;
	public float CritDMGGrowth;
	public float MoveSpeedGrowth;
	public float AttackSpeedGrowth;
	public static List<GrowthValueTable> Configs = new List<GrowthValueTable>();

	public GrowthValueTable() { }
	public GrowthValueTable(int Id, float HpGrowth, float MpGrowth, float ATKGrowth, float DEFGrowth, float CritGrowth, float CritDMGGrowth, float MoveSpeedGrowth, float AttackSpeedGrowth)
	{
		this.Id = Id;
		this.HpGrowth = HpGrowth;
		this.MpGrowth = MpGrowth;
		this.ATKGrowth = ATKGrowth;
		this.DEFGrowth = DEFGrowth;
		this.CritGrowth = CritGrowth;
		this.CritDMGGrowth = CritDMGGrowth;
		this.MoveSpeedGrowth = MoveSpeedGrowth;
		this.AttackSpeedGrowth = AttackSpeedGrowth;
	}
	public virtual GrowthValueTable Clone()
	{
		var config = new GrowthValueTable();
		this.MergeFrom(config);
		return config;
	}
	public virtual GrowthValueTable MergeFrom(GrowthValueTable source)
	{
		this.Id = source.Id;
		this.HpGrowth = source.HpGrowth;
		this.MpGrowth = source.MpGrowth;
		this.ATKGrowth = source.ATKGrowth;
		this.DEFGrowth = source.DEFGrowth;
		this.CritGrowth = source.CritGrowth;
		this.CritDMGGrowth = source.CritDMGGrowth;
		this.MoveSpeedGrowth = source.MoveSpeedGrowth;
		this.AttackSpeedGrowth = source.AttackSpeedGrowth;
		return this;
	}
	static GrowthValueTable()
	{
		Configs = new List<GrowthValueTable>
		{
			new GrowthValueTable
			{
				Id = 0,
				HpGrowth = 10f,
				MpGrowth = 5f,
				ATKGrowth = 5f,
				DEFGrowth = 3f,
				CritGrowth = 0.001f,
				CritDMGGrowth = 0.1f,
				MoveSpeedGrowth = 0.01f,
				AttackSpeedGrowth = 0.001f,
			},
			new GrowthValueTable
			{
				Id = 1,
				HpGrowth = 5f,
				MpGrowth = 3f,
				ATKGrowth = 3f,
				DEFGrowth = 1f,
				CritGrowth = 0f,
				CritDMGGrowth = 0f,
				MoveSpeedGrowth = 0f,
				AttackSpeedGrowth = 0f,
			},
			new GrowthValueTable
			{
				Id = 2,
				HpGrowth = 5f,
				MpGrowth = 3f,
				ATKGrowth = 3f,
				DEFGrowth = 1f,
				CritGrowth = 0f,
				CritDMGGrowth = 0f,
				MoveSpeedGrowth = 0f,
				AttackSpeedGrowth = 0f,
			},
		};
	}
	protected static Dictionary<int, GrowthValueTable> TempDictById;
	public static GrowthValueTable GetConfigById(int id)
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
