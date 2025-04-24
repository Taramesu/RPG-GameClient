using UnityEngine;
namespace RpgGame
{
    public class EntityDataUpdateEvent
    {
        public int typeId;
        public int id;
        public EntityData data;
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