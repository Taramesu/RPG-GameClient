using System.Security.Cryptography.X509Certificates;
using UnityEngine.PlayerLoop;

namespace RpgGame
{
    public partial class EntityData
    {
        public int id;

        public string name;

        public int exp;

        public PropertyData property;
    }

    public partial class EntityData
    {
        public int level;

        public EntityData(int id)
        {
            var config = EntityTable.GetConfigById(id);
            this.id = id;
            name = config.Name;
            exp = 0;
            property = new PropertyData(id);
        }
    }
}