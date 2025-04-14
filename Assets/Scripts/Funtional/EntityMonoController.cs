using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class EntityMonoController : MonoBehaviour, IController
    {
        private int typeId;
        private int id;
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
            transform.position = context.position;
        }
    }
}