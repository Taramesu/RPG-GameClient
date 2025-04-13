using UnityEngine;
using System;

namespace RpgGame
{
    public class CommonMono : MonoBehaviour
    {
        private static Action mUpdateAction;
        public static void AddUpdateAction(Action fun) => mUpdateAction += fun;
        public static void RemoveUpdateAction(Action fun) => mUpdateAction -= fun;

        private static Action mFixedUpadateAction;
        public static void AddFixedUpdateAction(Action fun) => mFixedUpadateAction += fun;
        public static void RemoveFixedUpdateAction(Action fun) => mFixedUpadateAction -= fun;

        private static Action mLateUpdateAction;
        public static void AddLateUpdateAction(Action fun) => mLateUpdateAction += fun;
        public static void RemoveLateUpdateAction(Action fun) => mLateUpdateAction -= fun;

        // Update is called once per frame
        private void Update()
        {
            mUpdateAction?.Invoke();
        }

        private void FixedUpdate()
        {
            mFixedUpadateAction?.Invoke();
        }

        private void LateUpdate() 
        {
            mLateUpdateAction?.Invoke();
        }
    }
}

