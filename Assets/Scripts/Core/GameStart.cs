using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class GameStart : MonoBehaviour, IController
    {
        private void Start()
        {
            //Æô¶¯¿ò¼Ü
            GetArchitecture();
        }

        public IArchitecture GetArchitecture() => RpgGame.Interface;
    }
}
