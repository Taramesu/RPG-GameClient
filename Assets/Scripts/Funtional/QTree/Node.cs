using QFramework;
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
                childList = null;
            }
        }

        public bool RemoveObj(ObjData obj)
        {
            //Debug.Log($"����Ϊ{depth}���ӽڵ���Ϊ{(childList == null ? 0 : childList.Length)}");

            // �����ǰ�ڵ�����ö������Ƴ�
            if (objList.Contains(obj))
            {
                //Debug.Log($"{depth}��ɹ��Ƴ�һ��obj");
                objList.Remove(obj);
                return true;
            }

            // ������ӽڵ㣬�ݹ�ɾ��
            if (childList != null)
            {
                bool found = false;
                for (int i = 0; i < childList.Length; ++i)
                {
                    //Debug.Log($"ִ��{depth}���{i}���ӽڵ���");
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
            // �����ǰ�ڵ����ӽڵ㣬���������ӽڵ�� objList ��Ϊ��
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

                // ��������ӽڵ㶼Ϊ�գ������ӽڵ�û���Լ����ӽڵ�
                if (allEmpty)
                {
                    childList = null;
                }
            }
        }

        public List<ObjData> QueryBounds(Bounds queryBound)
        {
            List<ObjData> result = new List<ObjData>();

            // ��鵱ǰ�ڵ�ı߽��Ƿ����ѯ�߽��ཻ
            if (!bound.Intersects(queryBound))
            {
                return result;
            }

            // ��ӵ�ǰ�ڵ��е����ж���
            result.AddRange(objList);

            // ������ӽڵ㣬�ݹ��ѯ
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


            ////ˢ�µ�ǰ�ڵ�
            //for (int i = 0; i < objList.Count; ++i)
            //{
            //    //�������иýڵ㱣�������
            //    ResourcesManager.Instance.LoadAsync(objList[i]);
            //}

            //if (depth == 0)
            //{
            //    ResourcesManager.Instance.RefreshStatus();
            //}

            ////ˢ���ӽڵ�
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
            // ��� obj �Ƿ��ڵ�ǰ�ڵ�� objList ��
            if (!objList.Contains(obj))
            {
                return false;
            }

            // ��� obj ����λ���Ƿ��ڵ�ǰ�ڵ�İ�Χ�з�Χ��
            if (!bound.Contains(obj.transform.position))
            {
                return false;
            }

            // ����Ƿ���Խ� obj ���䵽�ӽڵ���
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
                // ������ܷ��䵽�ӽڵ��У��򷵻� true
                return true;
            }
        }

        public Node FindNode(ObjData obj)
        {
            // ��鵱ǰ�ڵ��Ƿ���� obj
            if (objList.Contains(obj))
            {
                return this;
            }

            // ������ӽڵ㣬�ݹ����
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

            // δ�ҵ� obj
            return null;
        }

        public void TraverseAndPrint(int currentDepth = 0)
        {
            // �����ǰ�ڵ����Ϣ
            Debug.Log($"�������Ϊ {currentDepth}���˽ڵ㺬�� {(childList != null ? childList.Length : 0)} ���ӽڵ㣬�˽ڵ㺬�� {objList.Count} �� ObjData");

            // ������ӽڵ㣬�ݹ�����ӽڵ�
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