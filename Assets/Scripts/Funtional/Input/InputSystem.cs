using QFramework;
using UnityEngine.InputSystem;

namespace RpgGame
{
    public class InputSystem : AbstractSystem
    {
        protected override void OnInit() => CommonMono.AddUpdateAction(OnUpdate);

        private void OnUpdate()
        {
            Keyboard keyboard = Keyboard.current;
            MoveInput(keyboard);
        }

        private void MoveInput(Keyboard keyboard)
        {
            if(keyboard.wKey.isPressed)
            {
                this.SendEvent(new ControlEvent { id = 0, Control = ControlEnum.forward });
            }

            if (keyboard.sKey.isPressed)
            {
                this.SendEvent(new ControlEvent { id = 0, Control = ControlEnum.backward });
            }

            if (keyboard.aKey.isPressed)
            {
                this.SendEvent(new ControlEvent {id = 0, Control = ControlEnum.left });
            }

            if (keyboard.dKey.isPressed)
            {
                this.SendEvent(new ControlEvent {id = 0, Control = ControlEnum.right });
            }
        }
    }
}

