using UnityEngine;
namespace RpgGame
{
    public class EntityDataUpdateEvent
    {
        public int typeId;
        public int id;
        public EntityData data;
    }

    public class EntityHpUpdateEvent
    {
        public string sUid;
        public float Hp;
    }

    public class EntityMpUpdateEvent
    {
        public string sUid;
        public float Mp;
    }

    public class EntityPositionUpdateEvent
    {
        public string sUid;
        public Vector3 position;
    }

    public class EntityRotationUpdateEvent
    {
        public string sUid;
        public Quaternion rotation;
    }
}