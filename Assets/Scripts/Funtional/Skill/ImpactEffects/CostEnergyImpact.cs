using QFramework;

namespace RpgGame.Skill
{
    public class CostEnergyImpact : IImpactEffect, ICanGetModel
    {
        public void Execute(SkillDeployer deployer)
        {
            var sUid = deployer.SkillData.owner.GetComponent<ObjMonoController>().GetsUid();
            var data = this.GetModel<EntityModel>().GetData(sUid);
            data.property.Mp -= deployer.SkillData.costEnergy;
        }

        public IArchitecture GetArchitecture() => RpgGame.Interface;
    }
}