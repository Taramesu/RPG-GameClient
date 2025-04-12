using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class MoveSystem : AbstractSystem, ICanGetModel
    {
        protected override void OnInit()
        {
            this.RegisterEvent<ControlEvent>(OnMoveEvent);
            CommonMono.AddFixedUpdateAction(OnUpdate);
        }

        private void OnMoveEvent(ControlEvent e)
        {
            if (e == null) return;
            
            var mModel = this.GetModel<TransFormModel>();
            if(e.Control == ControlEnum.forward) 
            {
                var value = new Vector3(1,0,0);
                mModel.ChangePosition(e.id, value);
            }
            if (e.Control == ControlEnum.backward)
            {
                var value = new Vector3(-1, 0, 0);
                mModel.ChangePosition(e.id, value);
            }
            if (e.Control == ControlEnum.left)
            {
                var value = new Vector3(0, 0, 1);
                mModel.ChangePosition(e.id, value);
            }
            if (e.Control == ControlEnum.right)
            {
                var value = new Vector3(0, 0, -1);
                mModel.ChangePosition(e.id, value);
            }
        }

        private void OnUpdate()
        {

        }

        protected override void OnDeinit()
        {
            this.UnRegisterEvent<ControlEvent>(OnMoveEvent);
        }
    }
}