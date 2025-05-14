using System.Collections.Generic;
using UnityEngine;

namespace RpgGame
{
    public interface INode
    {
        //包围盒数据
        Bounds bound { get; set; }

        //插入数据节点
        void InsertObj(ObjData obj);

        //移除数据节点
        bool RemoveObj(ObjData obj);

        //查询指定包围盒范围内的所有存储的数据
        List<ObjData> QueryBounds(Bounds bound);

        //移动触发，取消指定摄像机视锥范围外所有对象的激活状态
        void TriggerMove(Camera camera);

        //实时绘画包围盒开发辅助线
        void DrawBound();
    }
}