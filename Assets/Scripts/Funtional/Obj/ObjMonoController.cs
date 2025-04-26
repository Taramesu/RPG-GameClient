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
            //this.RegisterEvent<EntityRotationUpdateEvent>(OnRotationChange);
            lastPosition = gameObject.transform.position;
        }

        private void Update()
        {
            PositionCheck();
        }

        private void OnDestroy()
        {
            this.UnRegisterEvent<EntityPositionUpdateEvent>(OnPositionChange);
            //this.UnRegisterEvent<EntityRotationUpdateEvent>(OnRotationChange);
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

    //    private void OnRotationChange(EntityRotationUpdateEvent context)
    //    {
    //        if(context.sUid == sUid)
    //            transform.rotation = context.rotation;
    //    }
    }
}