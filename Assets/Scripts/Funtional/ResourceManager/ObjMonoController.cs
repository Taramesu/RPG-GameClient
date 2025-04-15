using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class ObjMonoController : MonoBehaviour, IController
    {
        public string sUid;
        public IArchitecture GetArchitecture()
        {
            return RpgGame.Interface;
        }

        private void Start()
        {
            this.RegisterEvent<EntityPositionUpdateEvent>(OnPositionChange);
        }

        private void OnDestroy()
        {
            this.UnRegisterEvent<EntityPositionUpdateEvent>(OnPositionChange);
        }

        private void OnPositionChange(EntityPositionUpdateEvent context)
        {
            if(context.sUid == sUid)
            transform.position = context.position;
        }
    }
}