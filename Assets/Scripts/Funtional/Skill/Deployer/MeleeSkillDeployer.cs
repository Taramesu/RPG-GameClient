using UnityEngine;

namespace RpgGame.Skill
{
    public class MeleeSkillDeployer : SkillDeployer
    {
        public override void DeploySkill()
        {
            CalculateTargets();
            ImpactTargets();
        }

        private void OnDrawGizmos()
        {
            // 绘制扇形范围
            DrawSkillRange(transform.rotation);
        }

        private void DrawSkillRange(Quaternion rotation)
        {
            float resolution = 20f;

            // 确定绘制的起始点（技能释放位置）
            Vector3 center = transform.position;

            // 计算扇形两边的边界角度
            float halfAngle = SkillData.attackAngle / 2f;

            // 在扇形的起始边界和结束边界上各绘制一条线段，与中心点相连，来形成扇形的两条边
            Vector3 startEdgeEnd = center + rotation * Quaternion.Euler(0, -halfAngle, 0) * Vector3.forward * SkillData.attackDistance;
            Vector3 endEdgeEnd = center + rotation * Quaternion.Euler(0, halfAngle, 0) * Vector3.forward * SkillData.attackDistance;

            Gizmos.DrawLine(center, startEdgeEnd);
            Gizmos.DrawLine(center, endEdgeEnd);

            // 绘制扇形的弧线部分，通过多个小线段近似弧线
            for (int i = 0; i <= resolution; i++)
            {
                float currentAngle = Mathf.Lerp(-halfAngle, halfAngle, i / resolution);
                Vector3 currentPoint = center + rotation * Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * SkillData.attackDistance;
                Vector3 nextPoint = center + rotation * Quaternion.Euler(0, currentAngle + (SkillData.attackAngle / resolution), 0) * Vector3.forward * SkillData.attackDistance;

                Gizmos.DrawLine(currentPoint, nextPoint);
            }
        }
    }
}