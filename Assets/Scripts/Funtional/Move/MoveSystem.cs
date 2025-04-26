using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class MoveSystem : AbstractSystem, ICanGetModel
    {
        protected override void OnInit()
        {
            var manager = InputManager.Instance;
        }

        public void Move(string sUid, Vector3 dir)
        {
            var eModel = this.GetModel<EntityModel>();
            if (eModel == null) return;
            var value = dir * eModel.GetData(sUid).property.moveSpeed * Time.deltaTime;
            eModel.UpdatePosition(sUid, value);
            //eModel.UpdateRotation(sUid, Quaternion.LookRotation(dir));
        }

        protected override void OnDeinit()
        {

        }
    }
}