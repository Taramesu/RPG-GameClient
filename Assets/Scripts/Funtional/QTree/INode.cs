using System.Collections.Generic;
using UnityEngine;

namespace RpgGame
{
    public interface INode
    {
        Bounds bound { get; set; }

        void InsertObj(ObjData obj);

        bool RemoveObj(ObjData obj);

        List<ObjData> QueryBounds(Bounds bound);

        void TriggerMove(Camera camera);

        void DrawBound();
    }
}