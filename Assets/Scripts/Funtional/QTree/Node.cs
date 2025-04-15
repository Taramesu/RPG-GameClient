using System.Collections.Generic;
using UnityEngine;

namespace RpgGame
{
    public class Node : INode
    {
        //������
        public Bounds bound { get; set; }
        private List<ObjData> objList;

        //��ϵ��
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
                //δ��Ҷ�ӽڵ㣬����ӵ���ӽڵ����ӽڵ�δ�������򴴽��ӽڵ�
                CreateChild();
            }

            if (childList != null) //����Ҷ�ӽڵ�ʱchildListΪnull
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
                        //��֮ǰ���й�����������һ���ӽڵ�Ĺ�������
                        if (node != null)
                        {
                            bChild = false;
                            break;
                        }
                        //�״��ҵ�һ���ӽڵ㣬��Ͻ����
                        node = item;
                        bChild = true;
                    }
                }
            }

            //����������ȫ������һ���ӽڵ�
            if (bChild)
            {
                node.InsertObj(obj);
            }
            else //��������ڶ���ӽڵ㣬�������������ڵ�
            {
                objList.Add(obj);
            }
        }

        public void TriggerMove(Camera camera)
        {
            //ˢ�µ�ǰ�ڵ�
            for (int i = 0; i < objList.Count; ++i)
            {
                //�������иýڵ㱣�������
                ResourcesManager.Instance.LoadAsync(objList[i]);
            }

            if (depth == 0)
            {
                ResourcesManager.Instance.RefreshStatus();
            }

            //ˢ���ӽڵ�
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