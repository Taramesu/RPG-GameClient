using UnityEngine;

namespace RpgGame
{
    public partial class EntityData
    {
        public int id;

        public int typeId;

        public string name;

        public int exp;

        public bool enable;

        public PropertyData property;

        public TransformData transform;
    }

    public partial class EntityData
    {
        public int level;

        public EntityData(int typeId, TransformData transform)
        {
            var config = EntityTable.GetConfigById(typeId);
            this.typeId = typeId;
            name = config.Name;
            exp = 0;
            enable = true;
            this.transform = transform;
            property = new PropertyData(typeId);
        }
    }
}