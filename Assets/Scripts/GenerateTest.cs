using QFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RpgGame
{
    public class GenerateTest : MonoSingleton<GenerateTest>, IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return RpgGame.Interface;
        }

        public int offset = 1;
        public List<ObjData> objList = new List<ObjData>();
        public Bounds mainBound;
        public bool isDebug;
        public Tree tree;

        private bool bInitEnd = false;
        private Keyboard keyboard;

        private void Awake()
        {
            tree = new Tree(mainBound);

            bInitEnd = true;
        }

        private void Start()
        {
            keyboard = Keyboard.current;
        }

        private void OnEnable()
        {
            this.RegisterEvent<EntityGenerateEvent>(OnEntityGenerate);
            this.RegisterEvent<EntityCollectEvent>(OnEntityCollect);
            this.RegisterEvent<ObjPositionChange>(OnObjPositionChange);
        }

        private void OnDisable()
        {
            this.UnRegisterEvent<EntityGenerateEvent>(OnEntityGenerate);
            this.UnRegisterEvent<EntityCollectEvent>(OnEntityCollect);
            this.UnRegisterEvent<ObjPositionChange>(OnObjPositionChange);
        }

        private void Update()
        {
            if(keyboard.gKey.wasPressedThisFrame)
            {
                var es = this.GetSystem<EntitySystem>();
                int typeId = offset % 3 == 0 ? 1 : 2;
                var pos = new Vector3 (5, 0, offset++);
                var data = new TransformData { position = pos, rotation = Quaternion.identity, scale = Vector3.one };
                var go = es.GenerateEntity(typeId,data);
                //es.CollectEntity(go, 2000);
            }

            if(keyboard.tKey.wasReleasedThisFrame)
            {
                tree.TraverseAndPrint();
            }
        }

        private void OnDrawGizmos()
        {
            if(!isDebug) 
            {
                return;
            }

            if(bInitEnd) 
            {
                tree.DrawBound();
            }
            else
            {
                Gizmos.DrawWireCube(mainBound.center, mainBound.size);
            }

            var mCamera = Camera.main;

            Gizmos.color = Color.yellow;
            Matrix4x4 temp = Gizmos.matrix;

            // 使用摄像机的实际旋转来创建矩阵
            Gizmos.matrix = Matrix4x4.TRS(
                mCamera.transform.position,
                mCamera.transform.rotation,
                Vector3.one
            );

            if (mCamera.orthographic)
            {
                float spread = mCamera.farClipPlane - mCamera.nearClipPlane;
                float center = (mCamera.farClipPlane + mCamera.nearClipPlane) * 0.5f;
                Gizmos.DrawWireCube(
                    new Vector3(0, 0, center),
                    new Vector3(
                        mCamera.orthographicSize * 2 * mCamera.aspect,
                        mCamera.orthographicSize * 2,
                        spread
                    )
                );
            }
            else
            {
                Gizmos.DrawFrustum(
                    Vector3.zero,
                    mCamera.fieldOfView,
                    mCamera.farClipPlane,
                    mCamera.nearClipPlane,
                    mCamera.aspect
                );
            }

            Gizmos.matrix = temp;
        }

        private void OnEntityGenerate(EntityGenerateEvent context)
        {
            objList.Add(context.data);
            tree.InsertObj(context.data);
        }

        private void OnEntityCollect(EntityCollectEvent context)
        {
            tree.RemoveObj(context.data);
        }

        private void OnObjPositionChange(ObjPositionChange context)
        {
            var model = this.GetModel<ObjModel>();

            var objData = model.GetData(context.sUid);
            tree.UpdateTree(objData);
            //tree.TriggerMove(Camera.main);
        }
    }
}