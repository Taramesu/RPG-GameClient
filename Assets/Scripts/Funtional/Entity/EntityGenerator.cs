//using QFramework;
//using UnityEngine;

//namespace RpgGame
//{
//    public class EntityGenerator : MonoBehaviour, IController, ICanGetModel
//    {
//        public IArchitecture GetArchitecture()
//        {
//            return RpgGame.Interface;
//        }

//        private void OnEnable()
//        {
//            this.RegisterEvent<EntityGenerateEvent>(OnGenerate);
//        }

//        private void OnDisable() 
//        {
//            this.UnRegisterEvent<EntityGenerateEvent>(OnGenerate);
//        }

//        private void OnGenerate(EntityGenerateEvent context)
//        {
//            if(context == null)
//            {
//                Debug.LogError("EntityGenerateEvent is null");
//                return;
//            }
//            var gameObj = Instantiate(context.prefab, context.transformData.position, context.transformData.rotation);
//            gameObj.GetComponent<EntityMonoController>().Init(context.typeId, context.id);
//        }
//    }
//}