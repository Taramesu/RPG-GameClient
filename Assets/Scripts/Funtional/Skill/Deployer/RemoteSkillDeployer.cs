

using QFramework;
using UnityEngine;

namespace RpgGame.Skill
{
    public class RemoteSkillDeployer : SkillDeployer
    {
        private Vector3 deployDir;
        public override void DeploySkill()
        {
            CalculateTargets();
            ImpactTargets();
            deployDir = (transform.rotation * Vector3.forward).normalized;
            transform.rotation = Quaternion.identity;
        }

        private void Update()
        {
            transform.position += deployDir * 1 * Time.deltaTime;
        }
    }
}