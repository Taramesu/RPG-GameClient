using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace RpgGame
{
    public interface IInputSystem : ISystem { }
    public class InputSystem : AbstractSystem, IInputSystem
    {
        protected override void OnInit() => CommonMono.AddUpdateAction(OnUpdate);

        private void OnUpdate()
        {
            Keyboard keyboard = Keyboard.current;
            if(keyboard.anyKey.wasPressedThisFrame)
            {
                foreach(var key in keyboard.allKeys)
                {
                    if(key.wasPressedThisFrame)
                    {
                        Debug.Log("按下的键是："+key.name);
                        ControlTrans(key);
                    }
                }
            }
        }

        private void ControlTrans(KeyControl key)
        {
            if(key.name == "w")
            {
                this.SendEvent(new ControlEvent { id = 0, Control = ControlEnum.forward });
                return;
            }

            if (key.name == "s")
            {
                this.SendEvent(new ControlEvent { id = 0, Control = ControlEnum.backward });
                return;
            }

            if (key.name == "a")
            {
                this.SendEvent(new ControlEvent {id = 0, Control = ControlEnum.left });
                return;
            }

            if (key.name == "d")
            {
                this.SendEvent(new ControlEvent {id = 0, Control = ControlEnum.right });
                return;
            }
        }
    }
}

