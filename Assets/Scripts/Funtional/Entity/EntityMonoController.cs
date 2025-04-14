using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class EntityMonoController : MonoBehaviour, IController
    {
        public int typeId;
        public int id;
        public IArchitecture GetArchitecture()
        {
            return RpgGame.Interface;
        }

        public void Init(int typeId, int id)
        {
            this.typeId = typeId;
            this.id = id;
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
            if(context.typeId == typeId && context.id == id)
            transform.position = context.position;
        }
    }
}