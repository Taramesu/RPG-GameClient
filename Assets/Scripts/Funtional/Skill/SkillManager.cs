using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgGame.Skill
{
    public class SkillManager : MonoBehaviour
    {
        public List<SkillData> skills;
        private ResLoader mResLoader;

        private void Awake()
        {
            mResLoader = ResLoader.Allocate();
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

            if(skillData != null
                && skillData.cdRemain <= 0
                /*&& skillData.costEnergy <= skillData.owner.GetComponent<>*/)
            {
                return skillData;
            }
            else
            {
                return null;
            }
        }

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
    }
}