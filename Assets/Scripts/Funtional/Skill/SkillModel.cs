using QFramework;
using System.Collections.Generic;

namespace RpgGame.Skill
{
    public class SkillModel : AbstractModel
    {
        public List<SkillData> datas;
        protected override void OnInit()
        {
            datas = new();
        }

        private void InitSkill(SkillData data)
        {
            if(data.prefabName != null)
            {
                //data.skillPrefab = ResourcesManager.Instance.Load(data.prefabName);
            }
        }
    }
}