using QFramework;
using System.Collections;
using UnityEngine;

namespace RpgGame.Skill
{
    public class DamageImpact : IImpactEffect, ICanGetModel
    {
        //技能数据
        private SkillData data;
        //实体数据管理模块
        private EntityModel entityModel;

        //效果执行逻辑
        public void Execute(SkillDeployer deployer)
        {
            data = deployer.SkillData;
            entityModel = this.GetModel<EntityModel>();
            deployer.StartCoroutine(RepeatDamage(deployer));
        }

        private IEnumerator RepeatDamage(SkillDeployer deployer)
        {
            float atkTime = 0;
            do
            {
                OnceDamage();
                yield return new WaitForSeconds(data.attackInterval);
                atkTime += data.attackInterval;
                deployer.CalculateTargets();//重新计算目标
            } while (atkTime < data.durationTime);
        }

        private void OnceDamage()
        {
            var sUid = data.owner.GetComponent<ObjMonoController>().GetsUid();
            var entityData = entityModel.GetData(sUid);
            float value = data.attackRatio * entityData.property.ATK;
            for(int i = 0; i < data.targets.Count; i++) 
            {
                //伤害方法
                var target = entityModel.GetData(data.targets[i].sUid);
                if (target.typeId == entityData.typeId) continue;
                target.property.Hp -= value;
                Debug.Log($"target : {target.name} remain HP : {target.property.Hp}");
            }
        }

        public IArchitecture GetArchitecture()
        {
            return RpgGame.Interface;
        }
    }
}