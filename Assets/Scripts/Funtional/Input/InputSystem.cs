using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace RpgGame
{
    public class InputSystem : AbstractSystem
    {
        protected override void OnInit() => CommonMono.AddUpdateAction(OnUpdate);

        private void OnUpdate()
        {
            Keyboard keyboard = Keyboard.current;
            //if (keyboard.anyKey.isPressed)
            //{
            //    Debug.Log("anykey is pressed now!");
            //    foreach (var key in keyboard.allKeys)
            //    {
            //        if (key.wasPressedThisFrame)
            //        {
            //            //Debug.Log("按下的键是："+key.name);
            //            ControlTrans(key);
            //        }
            //    }
            //}
            MoveInput(keyboard);
        }

        private void MoveInput(Keyboard keyboard)
        {
            if(keyboard.wKey.isPressed)
            {
                this.SendEvent(new ControlEvent { id = 0, Control = ControlEnum.forward });
                return;
            }

            if (keyboard.sKey.isPressed)
            {
                this.SendEvent(new ControlEvent { id = 0, Control = ControlEnum.backward });
                return;
            }

            if (keyboard.aKey.isPressed)
            {
                this.SendEvent(new ControlEvent {id = 0, Control = ControlEnum.left });
                return;
            }

            if (keyboard.dKey.isPressed)
            {
                this.SendEvent(new ControlEvent {id = 0, Control = ControlEnum.right });
                return;
            }
        }
    }
}

