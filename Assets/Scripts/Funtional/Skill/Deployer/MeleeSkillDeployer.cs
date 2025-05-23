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
            // �������η�Χ
            DrawSkillRange(transform.rotation);
        }

        private void DrawSkillRange(Quaternion rotation)
        {
            float resolution = 20f;

            // ȷ�����Ƶ���ʼ�㣨�����ͷ�λ�ã�
            Vector3 center = transform.position;

            // �����������ߵı߽�Ƕ�
            float halfAngle = SkillData.attackAngle / 2f;

            // �����ε���ʼ�߽�ͽ����߽��ϸ�����һ���߶Σ������ĵ����������γ����ε�������
            Vector3 startEdgeEnd = center + rotation * Quaternion.Euler(0, -halfAngle, 0) * Vector3.forward * SkillData.attackDistance;
            Vector3 endEdgeEnd = center + rotation * Quaternion.Euler(0, halfAngle, 0) * Vector3.forward * SkillData.attackDistance;

            Gizmos.DrawLine(center, startEdgeEnd);
            Gizmos.DrawLine(center, endEdgeEnd);

            // �������εĻ��߲��֣�ͨ�����С�߶ν��ƻ���
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