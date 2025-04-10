using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class GameStart : MonoBehaviour, IController
    {
        private void Start()
        {
            //�������
            GetArchitecture();
        }

        public IArchitecture GetArchitecture() => RpgGame.Interface;
    }
}
