using QFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RpgGame.Skill
{
    public class SectorSelector : ISelector, IController
    {
        public IArchitecture GetArchitecture()
        {
            return RpgGame.Interface;
        }

        public List<ObjData> SelectTarget(SkillData data, Transform skillTF)
        {
            //�����ɹ���Ŀ��
            var tree = GenerateTest.Instance.tree;
            var ex = new Vector3(data.attackDistance, data.attackDistance, data.attackDistance);
            Bounds bound = new() { center = skillTF.position, extents = ex };
            var targets = tree.QueryBounds(bound);

            //�жϹ�����Χ
            targets = targets.FindAll(o => 
                Vector3.Distance(o.transform.position, skillTF.position) <= data.attackDistance && 
                Vector3.Angle(skillTF.forward, o.transform.position - skillTF.position) <= data.attackAngle/2
            );

            //�жϴ��
            targets = targets.FindAll(o =>
                this.GetModel<EntityModel>().GetData(o.sUid).property.Hp > 0
            );

            //�жϵ���/Ⱥ��
            if(data.attackType == SkillAttackType.Group)
                return targets;

            var singleTarget = targets.OrderBy(o => Vector3.Distance(o.transform.position, skillTF.position)).FirstOrDefault();
            return new List<ObjData>() { singleTarget };
        }
    }
}