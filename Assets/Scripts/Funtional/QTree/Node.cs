using System.Collections.Generic;
using UnityEngine;

namespace RpgGame
{
    public class Node : INode
    {
        //数据域
        public Bounds bound { get; set; }
        private List<ObjData> objList;

        //关系域
        private int depth;
        private Tree belongTree;
        private Node[] childList;

        public Node(Bounds bound, int depth, Tree belongTree)
        {
            this.belongTree = belongTree;
            this.depth = depth;
            this.bound = bound;

            objList = new List<ObjData>();
        }



        public void InsertObj(ObjData obj)
        {
            Node node = null;
            bool bChild = false;

            if (depth < belongTree.maxDepth && childList == null)
            {
                //未到叶子节点，可以拥有子节点且子节点未创建，则创建子节点
                CreateChild();
            }

            if (childList != null) //到达叶子节点时childList为null
            {
                for (int i = 0; i < childList.Length; ++i)
                {
                    Node item = childList[i];
                    if (item == null)
                    {
                        break;
                    }
                    if (item.bound.Contains(obj.transform.position))
                    {
                        //在之前已有管理，有属于另一个子节点的管理区域
                        if (node != null)
                        {
                            bChild = false;
                            break;
                        }
                        //首次找到一个子节点，管辖物体
                        node = item;
                        bChild = true;
                    }
                }
            }

            //插入物体完全归属于一个子节点
            if (bChild)
            {
                node.InsertObj(obj);
            }
            else //物体归属于多个子节点，将物体加入自身节点
            {
                objList.Add(obj);
            }
        }

        public void TriggerMove(Camera camera)
        {
            //刷新当前节点
            for (int i = 0; i < objList.Count; ++i)
            {
                //创建所有该节点保存的物体
                ResourcesManager.Instance.LoadAsync(objList[i]);
            }

            if (depth == 0)
            {
                ResourcesManager.Instance.RefreshStatus();
            }

            //刷新子节点
            if (childList != null)
            {
                for (int i = 0; i < childList.Length; ++i)
                {
                    if (childList[i].bound.CheckBoundIsInCamera(camera))
                    {
                        childList[i].TriggerMove(camera);
                    }
                }
            }
        }

        private void CreateChild()
        {
            childList = new Node[belongTree.maxChildCount];
            int index = 0;
            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                {
                    Vector3 centerOffset = new Vector3(bound.size.x / 4 * i, 0, bound.size.z / 4 * j);
                    Vector3 cSize = new Vector3(bound.size.x / 2, bound.size.y, bound.size.z / 2);
                    Bounds cBound = new Bounds(bound.center + centerOffset, cSize);
                    childList[index++] = new Node(cBound, depth + 1, belongTree);
                }
            }
        }

        public void DrawBound()
        {
            if (objList.Count != 0)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(bound.center, bound.size - Vector3.one * 0.1f);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(bound.center, bound.size - Vector3.one * 0.1f);
            }

            if (childList != null)
            {
                for (int i = 0; i < childList.Length; ++i)
                {
                    childList[i].DrawBound();
                }
            }
        }
    }
}