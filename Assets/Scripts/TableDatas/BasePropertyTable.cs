using System.Collections.Generic;
public class BasePropertyTable
{
	public int Id;
	public float BaseHp;
	public float BaseMp;
	public float BaseATK;
	public float BaseDEF;
	public float BaseCrit;
	public float BaseCritDMG;
	public float BaseMoveSpeed;
	public float BaseAttackSpeed;
	public static List<BasePropertyTable> Configs = new List<BasePropertyTable>();

	public BasePropertyTable() { }
	public BasePropertyTable(int Id, float BaseHp, float BaseMp, float BaseATK, float BaseDEF, float BaseCrit, float BaseCritDMG, float BaseMoveSpeed, float BaseAttackSpeed)
	{
		this.Id = Id;
		this.BaseHp = BaseHp;
		this.BaseMp = BaseMp;
		this.BaseATK = BaseATK;
		this.BaseDEF = BaseDEF;
		this.BaseCrit = BaseCrit;
		this.BaseCritDMG = BaseCritDMG;
		this.BaseMoveSpeed = BaseMoveSpeed;
		this.BaseAttackSpeed = BaseAttackSpeed;
	}
	public virtual BasePropertyTable Clone()
	{
		var config = new BasePropertyTable();
		this.MergeFrom(config);
		return config;
	}
	public virtual BasePropertyTable MergeFrom(BasePropertyTable source)
	{
		this.Id = source.Id;
		this.BaseHp = source.BaseHp;
		this.BaseMp = source.BaseMp;
		this.BaseATK = source.BaseATK;
		this.BaseDEF = source.BaseDEF;
		this.BaseCrit = source.BaseCrit;
		this.BaseCritDMG = source.BaseCritDMG;
		this.BaseMoveSpeed = source.BaseMoveSpeed;
		this.BaseAttackSpeed = source.BaseAttackSpeed;
		return this;
	}
	static BasePropertyTable()
	{
		Configs = new List<BasePropertyTable>
		{
			new BasePropertyTable
			{
				Id = 0,
				BaseHp = 100f,
				BaseMp = 20f,
				BaseATK = 10f,
				BaseDEF = 5f,
				BaseCrit = 0.05f,
				BaseCritDMG = 1.5f,
				BaseMoveSpeed = 0.1f,
				BaseAttackSpeed = 0.5f,
			},
			new BasePropertyTable
			{
				Id = 1,
				BaseHp = 50f,
				BaseMp = 10f,
				BaseATK = 10f,
				BaseDEF = 2f,
				BaseCrit = 0.01f,
				BaseCritDMG = 1.5f,
				BaseMoveSpeed = 0.05f,
				BaseAttackSpeed = 0.3f,
			},
			new BasePropertyTable
			{
				Id = 2,
				BaseHp = 20f,
				BaseMp = 10f,
				BaseATK = 5f,
				BaseDEF = 2f,
				BaseCrit = 0.01f,
				BaseCritDMG = 1.5f,
				BaseMoveSpeed = 0.1f,
				BaseAttackSpeed = 0.3f,
			},
		};
	}
	protected static Dictionary<int, BasePropertyTable> TempDictById;
	public static BasePropertyTable GetConfigById(int id)
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
