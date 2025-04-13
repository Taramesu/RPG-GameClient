using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class MoveSystem : AbstractSystem, ICanGetModel
    {
        protected override void OnInit()
        {
            this.RegisterEvent<ControlEvent>(OnMoveEvent);
        }

        private void OnMoveEvent(ControlEvent e)
        {
            if (e == null) return;

            var eModel = this.GetModel<EntityModel>();
            if (eModel == null) return;
            var value = e.dir * eModel.GetData(e.id).property.moveSpeed;

            var tModel = this.GetModel<TransFormModel>();
            if (tModel == null) return;
            tModel.ChangePosition(e.id, value);
        }

        public void Move(int id, Vector3 dir)
        {
            var eModel = this.GetModel<EntityModel>();
            if (eModel == null) return;
            var value = dir * eModel.GetData(id).property.moveSpeed * Time.deltaTime;

            var tModel = this.GetModel<TransFormModel>();
            if (tModel == null) return;
            tModel.ChangePosition(id, value);
        }

        protected override void OnDeinit()
        {
        }
    }
}