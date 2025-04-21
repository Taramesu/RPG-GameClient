using System.Collections.Generic;
using UnityEngine;

namespace RpgGame.Skill
{
    public abstract class SkillDeployer : MonoBehaviour
    {
        private SkillData skillData;
        public SkillData SkillData
        {
            get { return skillData; }
            set
            {
                skillData = value;
                InitDeployer();
            }
        }

        private ISelector selector;
        private List<IImpactEffect> impacts;

        //��ʼ���ͷ���
        private void InitDeployer()
        {
            //�����㷨����
            //ѡ��
            selector = DeployerConfigFactory.CreateSelector(skillData);
            //Ӱ��
            impacts = DeployerConfigFactory.CreateImpact(skillData);
        }

        /// <summary>
        /// ѡȡ����
        /// </summary>
        public void CalculateTargets()
        {
            skillData.targets = selector.SelectTarget(skillData, transform);
        }

        public void ImpactTargets()
        {
            foreach(var impact in  impacts) 
            {
                impact.Execute(this);
            }
        }

        public abstract void DeploySkill();
    }
}