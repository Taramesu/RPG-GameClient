using UnityEngine;

namespace RpgGame
{
    public enum SceneObjStatus
    {
        Old,//这次刷新没有加载
        Loading,//加载中
        New,//这次刷新加载
    }

    /// <summary>
    /// 场景物体
    /// </summary>
    public class SceneObj
    {
        public ObjData data;
        public SceneObjStatus status;
        public GameObject obj;

        public SceneObj(ObjData data)
        {
            this.data = data;
            this.obj = null;
        }
    }

    public class ResourcesObj
    {
        public GameObject obj;
        private int insNum;
        public ResourcesObj(GameObject obj)
        {
            this.obj = obj;
            this.insNum = 0;
        }

        public void CreateIns()
        {
            ++insNum;
        }

        public void DelIns()
        {
            --insNum;
        }

        public bool CheckInsZero()
        {
            return insNum <= 0;
        }
    }
}