using System;
using System.Collections.Generic;

namespace RpgGame.Skill
{
    public class DeployerConfigFactory
    {
        public static ISelector CreateSelector(SkillData data)
        {
            var classNameSelector = string.Format("RpgGame.Skill.{0}Selector", data.selectorType);
            return CreateObject<ISelector>(classNameSelector);
        }

        public static List<IImpactEffect> CreateImpact(SkillData data)
        {
            var impacts = new List<IImpactEffect>(data.impactType.Count);
            for(int i = 0; i < data.impactType.Count; i++) 
            {
                var classNameImpact = string.Format("RpgGame.Skill.{0}Impact", data.impactType[i]);
                impacts.Add(CreateObject<IImpactEffect>(classNameImpact));
            }
            return impacts;
        }

        private static T CreateObject<T>(string className) where T : class 
        {
            Type type = Type.GetType(className);
            return Activator.CreateInstance(type) as T;
        }
    }
}