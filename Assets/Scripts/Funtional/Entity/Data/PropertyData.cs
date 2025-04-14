namespace RpgGame
{
    public partial class PropertyData
    {
        public float Hp;

        public float Mp;

        public float ATK;

        public float DEF;

        public float crit;

        public float critDMG;

        public float moveSpeed;

        public float attackSpeed;
    }

    public partial class PropertyData
    {
        public PropertyData(int typeId) 
        {
            var config = BasePropertyTable.GetConfigById(typeId);
            Hp = config.BaseHp;
            Mp = config.BaseMp;
            ATK = config.BaseATK;
            DEF = config.BaseDEF;
            crit = config.BaseCrit;
            critDMG = config.BaseCritDMG;
            moveSpeed = config.BaseMoveSpeed;
            attackSpeed = config.BaseAttackSpeed;
        }
    }
}