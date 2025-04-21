using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class ObjMonoController : MonoBehaviour, IController, ICanSendEvent
    {
        [SerializeField]
        private string sUid;

        private Vector3 lastPosition;
        public IArchitecture GetArchitecture()
        {
            return RpgGame.Interface;
        }

        private void Start()
        {
            this.RegisterEvent<EntityPositionUpdateEvent>(OnPositionChange);
            lastPosition = gameObject.transform.position;
        }

        private void Update()
        {
            PositionCheck();
        }

        private void OnDestroy()
        {
            this.UnRegisterEvent<EntityPositionUpdateEvent>(OnPositionChange);
        }

        public void SetsUid(string value)
        {
            sUid = value;
        }

        public string GetsUid() 
        {
            return sUid;
        }

        private void PositionCheck()
        {
            if (transform.position != lastPosition)
            {
                lastPosition = transform.position;
                this.SendEvent(new ObjPositionChange() { sUid = sUid });
            }
        }

        private void OnPositionChange(EntityPositionUpdateEvent context)
        {
            if(context.sUid == sUid)
            transform.position = context.position;
        }
    }
}