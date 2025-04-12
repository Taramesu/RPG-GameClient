using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class CharacterMoveTest : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture()
        {
            return RpgGame.Interface;
        }

        private void Start()
        {
            this.RegisterEvent<TransFormDataChangeEvent>(OnTransformDataChange);
        }

        private void OnTransformDataChange(TransFormDataChangeEvent context)
        {
            transform.position = context.data.position;
            transform.rotation = context.data.rotation;
        }

        private void OnDestroy()
        {
            this.UnRegisterEvent<TransFormDataChangeEvent>(OnTransformDataChange);
        }
    }
}