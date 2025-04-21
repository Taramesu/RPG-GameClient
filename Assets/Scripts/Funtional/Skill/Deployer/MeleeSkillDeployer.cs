namespace RpgGame.Skill
{
    public class MeleeSkillDeployer : SkillDeployer
    {
        public override void DeploySkill()
        {
            CalculateTargets();
            ImpactTargets();
        }
    }
}