using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class CameraSystem : AbstractSystem, ICanGetModel
    {
        private TransformData followData;
        private Vector3 offset;
        private Vector3 velocity;
        private float smoothTime;

        private Camera mCamera;
        protected override void OnInit()
        {
            followData = this.GetModel<TransFormModel>().GetData(0);
            offset = new Vector3(0, 10, -10);
            smoothTime = 0.1f;
            velocity = Vector3.zero;
            mCamera = Camera.main;
            CommonMono.AddLateUpdateAction(CameraLock);
        }

        private void CameraLock()
        {
            var targetPosition = followData.position + offset;

            mCamera.transform.position = Vector3.SmoothDamp(mCamera.transform.position, targetPosition, ref velocity, smoothTime);

            mCamera.transform.rotation = Quaternion.Euler(45, 0, 0);
        }

        protected override void OnDeinit()
        {
            base.OnDeinit();
            CommonMono.RemoveLateUpdateAction(CameraLock);
        }
    }
}