using QFramework;
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
                childList = null;
            }
        }

        public bool RemoveObj(ObjData obj)
        {
            //Debug.Log($"本层为{depth}，子节点数为{(childList == null ? 0 : childList.Length)}");

            // 如果当前节点包含该对象，则移除
            if (objList.Contains(obj))
            {
                //Debug.Log($"{depth}层成功移除一个obj");
                objList.Remove(obj);
                return true;
            }

            // 如果有子节点，递归删除
            if (childList != null)
            {
                bool found = false;
                for (int i = 0; i < childList.Length; ++i)
                {
                    //Debug.Log($"执行{depth}层第{i}个子节点检查");
                    if (childList[i] != null)
                    {
                        found =  childList[i].RemoveObj(obj);
                        if(found == true)
                        {
                            TryMerge();
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void TryMerge()
        {
            // 如果当前节点有子节点，并且所有子节点的 objList 都为空
            if (childList != null)
            {
                bool allEmpty = true;
                for (int i = 0; i < childList.Length; ++i)
                {
                    if (childList[i] != null && childList[i].objList.Count > 0)
                    {
                        allEmpty = false;
                        break;
                    }
                    if (childList[i].childList != null)
                    {
                        allEmpty = false;
                        break;
                    }
                }

                // 如果所有子节点都为空，并且子节点没有自己的子节点
                if (allEmpty)
                {
                    childList = null;
                }
            }
        }

        public List<ObjData> QueryBounds(Bounds queryBound)
        {
            List<ObjData> result = new List<ObjData>();

            // 检查当前节点的边界是否与查询边界相交
            if (!bound.Intersects(queryBound))
            {
                return result;
            }

            // 添加当前节点中的所有对象
            result.AddRange(objList);

            // 如果有子节点，递归查询
            if (childList != null)
            {
                for (int i = 0; i < childList.Length; ++i)
                {
                    if (childList[i] != null)
                    {
                        result.AddRange(childList[i].QueryBounds(queryBound));
                    }
                }
            }

            return result;
        }

        public void TriggerMove(Camera camera)
        {


            ////刷新当前节点
            //for (int i = 0; i < objList.Count; ++i)
            //{
            //    //创建所有该节点保存的物体
            //    ResourcesManager.Instance.LoadAsync(objList[i]);
            //}

            //if (depth == 0)
            //{
            //    ResourcesManager.Instance.RefreshStatus();
            //}

            ////刷新子节点
            //if (childList != null)
            //{
            //    for (int i = 0; i < childList.Length; ++i)
            //    {
            //        if (childList[i].bound.CheckBoundIsInCamera(camera))
            //        {
            //            childList[i].TriggerMove(camera);
            //        }
            //    }
            //}
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

        public bool IsObjInBoundAndNotAssignableToChild(ObjData obj)
        {
            // 检查 obj 是否在当前节点的 objList 中
            if (!objList.Contains(obj))
            {
                return false;
            }

            // 检查 obj 的新位置是否在当前节点的包围盒范围内
            if (!bound.Contains(obj.transform.position))
            {
                return false;
            }

            // 检查是否可以将 obj 分配到子节点中
            bool bChild = false;
            bool hasNode = false;
            bool hasChild = true;

            if(depth < belongTree.maxChildCount && childList == null)
            {
                hasChild = false;
                CreateChild();
            }

            if (childList != null)
            {
                for (int i = 0; i < childList.Length; ++i)
                {
                    if (childList[i] != null && childList[i].bound.Contains(obj.transform.position))
                    {
                        if (hasNode)
                        {
                            bChild = false;
                            break;
                        }
                        hasNode = true;
                        bChild = true;
                    }
                }
            }

            if (!hasChild) 
            {
                childList = null;
            }

            if (bChild)
            {
                return false;
            }
            else
            {
                // 如果不能分配到子节点中，则返回 true
                return true;
            }
        }

        public Node FindNode(ObjData obj)
        {
            // 检查当前节点是否包含 obj
            if (objList.Contains(obj))
            {
                return this;
            }

            // 如果有子节点，递归查找
            if (childList != null)
            {
                for (int i = 0; i < childList.Length; ++i)
                {
                    if (childList[i] != null)
                    {
                        Node foundNode = childList[i].FindNode(obj);
                        if (foundNode != null)
                        {
                            return foundNode;
                        }
                    }
                }
            }

            // 未找到 obj
            return null;
        }

        public void TraverseAndPrint(int currentDepth = 0)
        {
            // 输出当前节点的信息
            Debug.Log($"本层深度为 {currentDepth}，此节点含有 {(childList != null ? childList.Length : 0)} 个子节点，此节点含有 {objList.Count} 个 ObjData");

            // 如果有子节点，递归遍历子节点
            if (childList != null)
            {
                for (int i = 0; i < childList.Length; ++i)
                {
                    if (childList[i] != null)
                    {
                        childList[i].TraverseAndPrint(currentDepth + 1);
                    }
                }
            }
        }

        public void DrawBound()
        {
            if (childList != null)
            {
                for (int i = 0; i < childList.Length; ++i)
                {
                    childList[i].DrawBound();
                }
            }

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
        }
    }
}