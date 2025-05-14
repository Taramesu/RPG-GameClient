using System.Collections.Generic;
using UnityEngine;

namespace RpgGame.Skill
{
    /// <summary>
    /// ����ѡ���ӿ�
    /// </summary>
    public interface ISelector
    {
        /// <summary>
        /// ����Ŀ��
        /// </summary>
        /// <param name="data">��������</param>
        /// <param name="skillTF">������������ı任���</param>
        /// <returns></returns>
        List<ObjData> SelectTarget(SkillData data, Transform skillTF);
    }
}