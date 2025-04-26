using Cysharp.Threading.Tasks;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgGame.Skill
{
    public class SkillManager : MonoBehaviour, ICanGetModel
    {
        public List<SkillData> skills;
        private ResLoader mResLoader;

        private void Awake()
        {
            mResLoader = ResLoader.Allocate();
            skills = new();
            foreach (var config in PlayerSkillTable.Configs)
            {
                var data = new SkillData
                {
                    id = config.Id,
                    name = config.Name,
                    description = config.Description,
                    cd = config.Cd,
                    costEnergy = config.CostEnergy,
                    attackDistance = config.AttackDistrance,
                    attackAngle = config.AttackAngle,
                    impactType = config.ImpactType,
                    attackRatio = config.AttackRatio,
                    durationTime = config.DurationTime,
                    attackInterval = config.AttackInterval,
                    prefabName = config.PrefabName,
                    animationName = config.AnimationName,
                    hitFxName = config.HitFxName,
                    attackType = config.SkillAttackType,
                    selectorType = config.SelectorType
                };
                skills.Add(data);
            }

            foreach (var skill in skills)
            {
                //InitSkill(skill);
            }
        }

        private void InitSkill(SkillData data)
        {
            if(data.prefabName != null)
            {
                data.skillPrefab = mResLoader.LoadSync<GameObject>(data.prefabName);
                data.owner = gameObject;
            }
        }

        /// <summary>
        /// 技能释放条件判断
        /// </summary>
        /// <param name="id">技能id</param>
        /// <returns></returns>
        public SkillData PrepareSkill(int id)
        {
            SkillData skillData = new();
            skillData = skills.Find(x => x.id == id);

            var model = this.GetModel<EntityModel>();
            var sUid = skillData.owner.GetComponent<ObjMonoController>().GetsUid();
            var entityData = model.GetData(sUid);

            if(skillData != null
                && skillData.cdRemain <= 0
                && skillData.costEnergy <= entityData.property.Mp)
            {
                return skillData;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 生成技能
        /// </summary>
        /// <param name="skillData"></param>
        public void GenerateSkill(SkillData skillData)
        {
            var skillGo = Pool.Instance.CreateObject(skillData.prefabName, skillData.skillPrefab, transform.position, transform.rotation);
            var deployer = skillGo.GetComponent<SkillDeployer>();
            deployer.SkillData = skillData;
            deployer.DeploySkill();
            Pool.Instance.CollectObject(skillGo, skillData.durationTime);
            StartCoroutine(CoolTimeDown(skillData));
        }

        private IEnumerator CoolTimeDown(SkillData data)
        {
            data.cdRemain = data.cd;
            while(data.cdRemain > 0)
            {
                yield return new WaitForSeconds(1f);
                data.cdRemain--;
            }
        }

        private void OnDestroy()
        {
            mResLoader.Recycle2Cache();
            mResLoader=null;
        }

        public IArchitecture GetArchitecture()
        {
            return RpgGame.Interface;
        }
    }
}