using UnityEngine;
using System;

namespace RpgGame
{
    public class CommonMono : MonoBehaviour
    {
        private static Action mUpdateAction;
        public static void AddUpdateAction(Action fun) => mUpdateAction += fun;
        public static void RemoveUpdateAction(Action fun) => mUpdateAction -= fun;

        // Update is called once per frame
        void Update()
        {
            mUpdateAction?.Invoke();
        }
    }
}

