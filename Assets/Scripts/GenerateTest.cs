using QFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RpgGame
{
    public class GenerateTest : MonoBehaviour, IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return RpgGame.Interface;
        }

        public int offset = 1;
        public List<ObjData> objList = new List<ObjData>();
        public Bounds mainBound;
        public bool isDebug;

        private Tree tree;
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
            this.RegisterEvent<ObjPositionChange>(OnObjPositionChange);
        }

        private void OnDisable()
        {
            this.UnRegisterEvent<EntityGenerateEvent>(OnEntityGenerate);
            this.UnRegisterEvent<ObjPositionChange>(OnObjPositionChange);
        }

        private void Update()
        {
            if(keyboard.jKey.wasPressedThisFrame)
            {
                var es = this.GetSystem<EntitySystem>();
                int typeId = offset % 3;
                var pos = new Vector3 (5, 0, offset++);
                var data = new TransformData { position = pos, rotation = Quaternion.identity, scale = Vector3.one };
                es.EntityGenerate(typeId,data);
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
        }

        private void OnEntityGenerate(EntityGenerateEvent context)
        {
            objList.Add(context.data);
            tree.InsertObj(context.data);
        }

        private void OnObjPositionChange(ObjPositionChange context)
        {
            var model = this.GetModel<ObjModel>();

            var objData = model.GetData(context.sUid);
            tree.UpdateTree(objData);
        }
    }
}