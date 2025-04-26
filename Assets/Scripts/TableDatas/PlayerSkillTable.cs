using System.Collections.Generic;
using RpgGame.Skill;
public class PlayerSkillTable
{
	public int Id;
	public string Name;
	public string Description;
	public int Cd;
	public int CostEnergy;
	public float AttackDistrance;
	public float AttackAngle;
	public List<string> ImpactType;
	public float AttackRatio;
	public float DurationTime;
	public float AttackInterval;
	public string PrefabName;
	public string AnimationName;
	public string HitFxName;
	public SkillAttackType SkillAttackType;
	public SelectorType SelectorType;
	public static List<PlayerSkillTable> Configs = new List<PlayerSkillTable>();

	public PlayerSkillTable() { }
	public PlayerSkillTable(int Id, string Name, string Description, int Cd, int CostEnergy, float AttackDistrance, float AttackAngle, List<string> ImpactType, float AttackRatio, float DurationTime, float AttackInterval, string PrefabName, string AnimationName, string HitFxName, SkillAttackType SkillAttackType, SelectorType SelectorType)
	{
		this.Id = Id;
		this.Name = Name;
		this.Description = Description;
		this.Cd = Cd;
		this.CostEnergy = CostEnergy;
		this.AttackDistrance = AttackDistrance;
		this.AttackAngle = AttackAngle;
		this.ImpactType = ImpactType;
		this.AttackRatio = AttackRatio;
		this.DurationTime = DurationTime;
		this.AttackInterval = AttackInterval;
		this.PrefabName = PrefabName;
		this.AnimationName = AnimationName;
		this.HitFxName = HitFxName;
		this.SkillAttackType = SkillAttackType;
		this.SelectorType = SelectorType;
	}
	public virtual PlayerSkillTable Clone()
	{
		var config = new PlayerSkillTable();
		this.MergeFrom(config);
		return config;
	}
	public virtual PlayerSkillTable MergeFrom(PlayerSkillTable source)
	{
		this.Id = source.Id;
		this.Name = source.Name;
		this.Description = source.Description;
		this.Cd = source.Cd;
		this.CostEnergy = source.CostEnergy;
		this.AttackDistrance = source.AttackDistrance;
		this.AttackAngle = source.AttackAngle;
		this.ImpactType = source.ImpactType;
		this.AttackRatio = source.AttackRatio;
		this.DurationTime = source.DurationTime;
		this.AttackInterval = source.AttackInterval;
		this.PrefabName = source.PrefabName;
		this.AnimationName = source.AnimationName;
		this.HitFxName = source.HitFxName;
		this.SkillAttackType = source.SkillAttackType;
		this.SelectorType = source.SelectorType;
		return this;
	}
	static PlayerSkillTable()
	{
		Configs = new List<PlayerSkillTable>
		{
			new PlayerSkillTable
			{
				Id = 1001,
				Name = "normalAttack1",
				Description = "进行一次平a",
				Cd = 1,
				CostEnergy = 0,
				AttackDistrance = 2f,
				AttackAngle = 130f,
				ImpactType = new List<string> {"Damage","SpeedDown"},
				AttackRatio = 0.5f,
				DurationTime = 0.5f,
				AttackInterval = 0.5f,
				PrefabName = " normalAttack1",
				AnimationName = "normalAttack1",
				HitFxName = "normalAttack1",
				SkillAttackType = SkillAttackType.Single,
				SelectorType = SelectorType.Sector,
			},
			new PlayerSkillTable
			{
				Id = 1002,
				Name = "normalAttack2",
				Description = "进行一次平a",
				Cd = 1,
				CostEnergy = 0,
				AttackDistrance = 2f,
				AttackAngle = 130f,
				ImpactType = new List<string> {"Damage","SpeedDown"},
				AttackRatio = 0.5f,
				DurationTime = 0.5f,
				AttackInterval = 0.5f,
				PrefabName = " normalAttack2",
				AnimationName = "normalAttack2",
				HitFxName = "normalAttack2",
				SkillAttackType = SkillAttackType.Single,
				SelectorType = SelectorType.Sector,
			},
		};
	}
	protected static Dictionary<int, PlayerSkillTable> TempDictById;
	public static PlayerSkillTable GetConfigById(int id)
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
