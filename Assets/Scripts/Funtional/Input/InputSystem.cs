using Cysharp.Threading.Tasks;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RpgGame
{
    public class InputSystem : AbstractSystem, ICanGetSystem
    {
        private Vector3 dir = Vector3.zero;
        private int dirCount = 0;
        private EntityData player;
        protected async override void OnInit()
        {
            await UniTask.WaitUntil(() => this.GetModel<EntityModel>().GetData(0) != null);
            player = this.GetModel<EntityModel>().GetData(0);
            CommonMono.AddUpdateAction(OnUpdate);
            var manager = InputManager.Instance;
            InputManager.RegisterKeyEvent(Key.W, Forward);
            InputManager.RegisterKeyEvent(Key.S, Backward);
            InputManager.RegisterKeyEvent(Key.A, Left);
            InputManager.RegisterKeyEvent(Key.D, Right);
            InputManager.RegisterKeyEvent(Key.W, CancelForward, 0,KeyEventType.KeyUp);
            InputManager.RegisterKeyEvent(Key.S, CancelBackward, 0, KeyEventType.KeyUp);
            InputManager.RegisterKeyEvent(Key.A, CancelLeft, 0, KeyEventType.KeyUp);
            InputManager.RegisterKeyEvent(Key.D, CancelRight, 0, KeyEventType.KeyUp);
            InputManager.RegisterKeyEvent(Key.J, Attack);
        }

        protected override void OnDeinit()
        {
            base.OnDeinit();
            InputManager.UnRegisterKeyEvent(Key.W, Forward);
            InputManager.UnRegisterKeyEvent(Key.S, Backward);
            InputManager.UnRegisterKeyEvent(Key.A, Left);
            InputManager.UnRegisterKeyEvent(Key.D, Right);
            InputManager.UnRegisterKeyEvent(Key.W, CancelForward);
            InputManager.UnRegisterKeyEvent(Key.S, CancelBackward);
            InputManager.UnRegisterKeyEvent(Key.A, CancelLeft);
            InputManager.UnRegisterKeyEvent(Key.D, CancelRight);
            InputManager.UnRegisterKeyEvent(Key.J, Attack);
        }

        private void OnUpdate()
        {
            Move();
        }

        private void Move()
        {
            if(dirCount > 0)
            {
                if(dir.magnitude > 0)
                {
                    this.GetSystem<MoveSystem>().Move(player.sUid, dir.normalized);
                }
            }
        }

        private void Attack()
        {
            this.SendEvent(new AttackEvent() { skillId = 1001 });
        }

        private void Forward()
        {
            dir += Vector3.forward;
            dirCount++;
        }

        private void CancelForward()
        {
            dir -= Vector3.forward;
            dirCount--;
        }

        private void Backward()
        {
            dir += Vector3.back;
            dirCount++;
        }

        private void CancelBackward()
        {
            dir -= Vector3.back;
            dirCount--;
        }

        private void Left() 
        {
            dir += Vector3.left;
            dirCount++;
        }

        private void CancelLeft()
        {
            dir -= Vector3.left;
            dirCount--;
        }

        private void Right()
        {
            dir += Vector3.right;
            dirCount++;
        }

        private void CancelRight()
        {
            dir -= Vector3.right;
            dirCount--;
        }
    }
}

