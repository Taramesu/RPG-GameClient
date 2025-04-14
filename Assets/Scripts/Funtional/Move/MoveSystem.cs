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
            var value = e.dir * eModel.GetData(e.typeId,e.id).property.moveSpeed;
            eModel.UpdatePosition(e.typeId, e.id, value);
        }

        public void Move(int typeId,int id, Vector3 dir)
        {
            var eModel = this.GetModel<EntityModel>();
            if (eModel == null) return;
            var value = dir * eModel.GetData(typeId,id).property.moveSpeed * Time.deltaTime;
            eModel.UpdatePosition(typeId, id, value);
        }

        protected override void OnDeinit()
        {
            this.UnRegisterEvent<ControlEvent>(OnMoveEvent);
        }
    }
}