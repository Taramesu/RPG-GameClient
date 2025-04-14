using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class GameStart : MonoBehaviour, IController, ICanGetSystem
    {
        private void Start()
        {
            //�������
            GetArchitecture();
            //������Դ����
            ResKit.Init();
            InitGame();
        }

        public IArchitecture GetArchitecture() => RpgGame.Interface;

        private void InitGame()
        {
            //player����
            var es = this.GetSystem<EntitySystem>();
            es.EntityGenerate(0, new TransformData { position = Vector3.zero, rotation = Quaternion.identity, scale = Vector3.one });
        }
    }
}
