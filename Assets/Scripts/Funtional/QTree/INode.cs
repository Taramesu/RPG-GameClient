using UnityEngine;

namespace RpgGame
{
    public interface INode
    {
        Bounds bound { get; set; }

        void InsertObj(ObjData obj);

        void TriggerMove(Camera camera);

        void DrawBound();
    }
}