using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RpgGame
{
    public class InputSystem : AbstractSystem, ICanGetSystem
    {
        protected override void OnInit() => CommonMono.AddUpdateAction(OnUpdate);

        private void OnUpdate()
        {
            Keyboard keyboard = Keyboard.current;
            MoveInput(keyboard);
        }

        private void MoveInput(Keyboard keyboard)
        {
            Vector3 dir = new Vector3(0, 0, 0);
            if (keyboard.wKey.isPressed)
            {
                dir += new Vector3(0, 0, 1);
            }

            if (keyboard.sKey.isPressed)
            {
                dir += new Vector3(0, 0, -1);
            }

            if (keyboard.aKey.isPressed)
            {
                dir += new Vector3(-1, 0, 0);
            }

            if (keyboard.dKey.isPressed)
            {
                dir += new Vector3(1, 0, 0);
            }
            //this.SendEvent(new ControlEvent { id = 0, dir = dir.normalized });
            this.GetSystem<MoveSystem>().Move(0, 0, dir.normalized);
        }
    }
}

