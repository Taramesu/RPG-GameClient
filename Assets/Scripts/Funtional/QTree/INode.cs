using System.Collections.Generic;
using UnityEngine;

namespace RpgGame
{
    public interface INode
    {
        //��Χ������
        Bounds bound { get; set; }

        //�������ݽڵ�
        void InsertObj(ObjData obj);

        //�Ƴ����ݽڵ�
        bool RemoveObj(ObjData obj);

        //��ѯָ����Χ�з�Χ�ڵ����д洢������
        List<ObjData> QueryBounds(Bounds bound);

        //�ƶ�������ȡ��ָ���������׶��Χ�����ж���ļ���״̬
        void TriggerMove(Camera camera);

        //ʵʱ�滭��Χ�п���������
        void DrawBound();
    }
}