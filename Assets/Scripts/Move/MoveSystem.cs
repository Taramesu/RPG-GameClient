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
            
            var mModel = this.GetModel<TransFormModel>();
            if(e.Control == ControlEnum.forward) 
            {
                var value = new Vector3(10,0,0);
                mModel.ChangePosition(e.id, value);
            }
        }

        protected override void OnDeinit()
        {
            this.UnRegisterEvent<ControlEvent>(OnMoveEvent);
        }
    }
}