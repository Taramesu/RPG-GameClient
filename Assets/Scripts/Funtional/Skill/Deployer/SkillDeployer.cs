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

        //初始化释放器
        private void InitDeployer()
        {
            //创建算法对象
            //选区
            selector = DeployerConfigFactory.CreateSelector(skillData);
            //影响
            impacts = DeployerConfigFactory.CreateImpact(skillData);
        }

        /// <summary>
        /// 选取敌人
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