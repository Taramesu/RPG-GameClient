using UnityEngine;

namespace RpgGame.Skill
{
    /// <summary>
    /// ��װ����ϵͳ
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
            //׼������
            SkillData skill = skillManager.PrepareSkill(skillId);
            if (skill == null) return;
            //���Ŷ���
            animator.SetBool(skill.animationName, true);
            //���ɼ���
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