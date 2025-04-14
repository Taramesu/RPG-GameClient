using UnityEngine;

namespace RpgGame
{
    public enum DirControlEnum
    {
        forward, backward, left, right
    }

    public class ControlEvent
    {
        public int typeId;
        public int id;
        public Vector3 dir;
        //public DirControlEnum Control;
    }
}
