using UnityEngine;

namespace RpgGame.Skill
{
    /// <summary>
    /// 封装技能系统
    /// </summary>
    [RequireComponent(typeof(SkillManager))]
    public class SkillController : MonoBehaviour
    {
        private SkillManager skillManager;
        private Animator animator;

        private void Start()
        {
            skillManager = GetComponent<SkillManager>();
            animator = GetComponentInChildren<Animator>();
            //GetComponentInChildren<AnimationEvent>().
        }

        public void AttackUseSkill(int skillId)
        {
            //准备技能
            SkillData skill = skillManager.PrepareSkill(skillId);
            if (skill == null) return;
            //播放动画
            animator.SetBool(skill.animationName, true);
            //生成技能
            skillManager.GenerateSkill(skill);
        }

        public void UseRandomSkill()
        {
            var usableSkills = skillManager.skills.FindAll(s => skillManager.PrepareSkill(s.id) != null);
            if (usableSkills.Count == 0) return;
            int index = Random.Range(0, usableSkills.Count);
            AttackUseSkill(usableSkills[index].id);
        }
    }
}