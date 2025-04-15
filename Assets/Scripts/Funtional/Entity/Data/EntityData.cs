using UnityEngine;

namespace RpgGame
{
    public partial class EntityData : ObjData
    {
        public int id;

        public int typeId;

        public int exp;

        public bool enable;

        public PropertyData property;
    }

    public partial class EntityData
    {
        public int level;

        public EntityData(int typeId, TransformData transform) : base(transform)
        {
            var config = EntityTable.GetConfigById(typeId);
            this.typeId = typeId;
            name = config.Name;
            exp = 0;
            enable = true;
            property = new PropertyData(typeId);
        }
    }
}