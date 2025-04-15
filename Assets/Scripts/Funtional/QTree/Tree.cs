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