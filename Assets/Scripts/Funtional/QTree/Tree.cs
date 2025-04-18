using System.Collections.Generic;
using UnityEngine;

namespace RpgGame
{
    public class Tree : INode
    {
        public Bounds bound { get; set; }

        private Node root;

        public int maxDepth { get; set; }

        public int maxChildCount { get; set; }

        public Tree(Bounds bound)
        {
            this.bound = bound;
            maxDepth = 5;
            maxChildCount = 4;

            root = new Node(bound, 0, this);
        }
        

        public void InsertObj(ObjData obj)
        {
            root.InsertObj(obj);
        }

        public bool RemoveObj(ObjData obj) 
        {
            return root.RemoveObj(obj);
        }

        public List<ObjData> QueryBounds(Bounds bound)
        {
            return root.QueryBounds(bound);
        }

        public bool CheckObjPosition(ObjData obj)
        {
            // 从根节点开始查找 obj 原所属的节点
            Node node = FindNode(obj);

            if (node == null)
            {
                // 树中没有这个 objData
                return false;
            }

            // 检查 obj 的新位置是否在应在节点的范围内
            return node.IsObjInBoundAndNotAssignableToChild(obj);
        }

        private Node FindNode(ObjData obj)
        {
            return root.FindNode(obj);
        }

        public void UpdateTree(ObjData obj) 
        {
            if(CheckObjPosition(obj))
            {
                return;
            }
            root.RemoveObj(obj);
            root.InsertObj(obj);
        }

        public void TraverseAndPrint()
        {
            root.TraverseAndPrint();
        }

        public void TriggerMove(Camera camera)
        {
            root.TriggerMove(camera);
        }

        public void DrawBound()
        {
            root.DrawBound();
        }
    }
}