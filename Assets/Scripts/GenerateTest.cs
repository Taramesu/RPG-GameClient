using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RpgGame
{
    public class GenerateTest : MonoBehaviour, ICanGetSystem
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return RpgGame.Interface;
        }

        public int offset = 1;
        public Keyboard keyboard;

        private void Start()
        {
            keyboard = Keyboard.current;
        }

        private void Update()
        {
            if(keyboard.jKey.wasPressedThisFrame)
            {
                var es = this.GetSystem<EntitySystem>();
                int typeId = offset % 3;
                var pos = new Vector3 (0, 0, offset++);
                var data = new TransformData { position = pos, rotation = Quaternion.identity, scale = Vector3.one };
                es.EntityGenerate(typeId,data);
            }
        }
    }
}