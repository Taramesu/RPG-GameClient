using System;
using UnityEngine;

namespace RpgGame.Skill
{
    [Serializable]
    public class SkillData
    {
        public int id;//����id
        public string name;//��������
        public string description;//��������
        public int cd;//������ȴʱ��
        public int cdRemain;//����ʣ����ȴʱ��
        public int costEnergy;//������������
        public float attackDistance;//��������
        public float attackAngle;//�����Ƕ�
        public string[] targetTags;//������Ŀ��tag
        [HideInInspector]
        public Transform[] targets;//����Ŀ���������
        public string[] impactType;//����Ӱ������
        public float attackRatio;//���ܱ���
        public float durationTime;//����ʱ��
        public float attackInterval;//�������
        [HideInInspector]
        public GameObject owner;//����������ɫ
        public string prefabName;//����Ԥ��������
        [HideInInspector]
        public GameObject skillPrefab;//����Ԥ�������
        public string animationName;//���ܶ�������
        public string hitFxName;//�ܻ���Ч����
        [HideInInspector]
        public GameObject hitFxPrefab;//�ܻ���Ч����
        public int level;//���ܵȼ�
        public SkillAttackType attackType;//��������
        public SelectorType selectorType;//ѡ����Χ���ͣ�Բ�Σ����Σ����εȣ�
    }

    public enum SkillAttackType
    {
        Single, Group
    }

    public enum SelectorType
    {
        Sector, Rectangle
    }
}