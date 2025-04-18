using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgGame
{
    public class ResourcesManager : MonoSingleton<ResourcesManager>, IController
    {
        public IArchitecture GetArchitecture()
        {
            return RpgGame.Interface;
        }

        //public static ResourcesManager Instance;

        public float delTime = 2;
        private Dictionary<string, SceneObj> activeObjDic;//<suid,SceneObj>
        private Dictionary<string, SceneObj> inActiveObjDic;//<suid,SceneObj>
        private List<string> delKeysList;

        private Dictionary<string, ResourcesObj> resourcesObjDic;//<resName,ResourcesObj>

        private ResLoader mResLoader = ResLoader.Allocate();
        
        #region get set
        public Dictionary<string, SceneObj> ActiveObjDic
        {
            get
            {
                if (activeObjDic == null)
                {
                    activeObjDic = new Dictionary<string, SceneObj>();
                }
                return activeObjDic;
            }
            set
            {
                activeObjDic = value;
            }
        }

        public Dictionary<string, SceneObj> InActiveObjDic
        {
            get
            {
                if (inActiveObjDic == null)
                {
                    inActiveObjDic = new Dictionary<string, SceneObj>();
                }
                return inActiveObjDic;
            }
            set
            {
                inActiveObjDic = value;
            }
        }

        public List<string> DelKeysList
        {
            get
            {
                if (delKeysList == null)
                {
                    delKeysList = new List<string>();
                }
                return delKeysList;
            }

            set
            {
                delKeysList = value;
            }
        }

        public Dictionary<string, ResourcesObj> ResourcesObjDic
        {
            get
            {
                if (resourcesObjDic == null)
                {
                    resourcesObjDic = new Dictionary<string, ResourcesObj>();
                }
                return resourcesObjDic;
            }

            set
            {
                resourcesObjDic = value;
            }
        }
        #endregion

        private void Awake()
        {
            //Instance = this;
        }

        private void OnEnable()
        {
            StartCoroutine(IEDel());
        }

        private IEnumerator IEDel()
        {
            while (true)
            {
                bool bDel = false;
                foreach (var pair in InActiveObjDic)
                {
                    ResourcesObj resourceObj;
                    if (ResourcesObjDic.TryGetValue(pair.Value.data.name, out resourceObj))
                    {
                        resourceObj.DelIns();
                        if (resourceObj.CheckInsZero())
                        {
                            bDel = true;
                            resourceObj.obj = null;
                            ResourcesObjDic.Remove(pair.Value.data.name);
                        }
                    }
                    Destroy(pair.Value.obj);
                }
                InActiveObjDic.Clear();
                if (bDel)
                {
                    mResLoader.Recycle2Cache();
                }
                yield return new WaitForSeconds(delTime);
            }
        }

        public SceneObj CheckIsActive(string sUid)
        {
            SceneObj obj;
            if (ActiveObjDic.TryGetValue(sUid, out obj))
            {
                return obj;
            }
            return null;
        }

        public SceneObj CheckIsInActive(string sUid)
        {
            SceneObj obj;
            if (InActiveObjDic.TryGetValue(sUid, out obj))
            {
                return obj;
            }
            return null;
        }

        private bool MoveToActive(ObjData obj)//把不启用的加入到启用的集合中
        {
            SceneObj sceneObj;
            if (InActiveObjDic.TryGetValue(obj.sUid, out sceneObj))
            {
                sceneObj.obj.SetActive(true);
                sceneObj.status = SceneObjStatus.New;
                ActiveObjDic.Add(obj.sUid, sceneObj);
                InActiveObjDic.Remove(obj.sUid);
                return true;
            }
            return false;
        }

        public bool MoveToInActive(ObjData obj)//把启用的加入到不启用的集合中
        {
            SceneObj sceneObj;
            if (ActiveObjDic.TryGetValue(obj.sUid, out sceneObj))
            {
                sceneObj.obj.SetActive(false);
                InActiveObjDic.Add(obj.sUid, sceneObj);
                ActiveObjDic.Remove(obj.sUid);
                return true;
            }
            return false;
        }

        private void CreateObj(GameObject prefab, SceneObj sceneObj)//生成物体
        {
            sceneObj.obj = Instantiate(prefab);
            sceneObj.obj.transform.position = sceneObj.data.transform.position;
            sceneObj.obj.transform.rotation = sceneObj.data.transform.rotation;
        }

        public void Load(ObjData obj)
        {
            if (CheckIsActive(obj.sUid) != null)
            {
                return;
            }
            if (!MoveToActive(obj))
            {
                SceneObj sceneObj = new SceneObj(obj);
                sceneObj.status = SceneObjStatus.New;

                GameObject resObj = null;
                ResourcesObj resourceObj;
                if (ResourcesObjDic.TryGetValue(obj.name, out resourceObj))
                {
                    resObj = resourceObj.obj;
                    resourceObj.CreateIns();
                }
                else
                {
                    resObj = mResLoader.LoadSync<GameObject>(obj.name);
                    resObj.GetComponent<ObjMonoController>().SetsUid(obj.sUid);
                }

                CreateObj(resObj, sceneObj);
                ActiveObjDic.Add(obj.sUid, sceneObj);
            }
        }
        public void LoadAsync(ObjData obj)
        {
            if (CheckIsActive(obj.sUid) != null)
            {
                return;
            }
            if (!MoveToActive(obj))
            {
                SceneObj sceneObj = new SceneObj(obj);
                sceneObj.status = SceneObjStatus.Loading;
                ActiveObjDic.Add(obj.sUid, sceneObj);
                GameObject resObj = null;
                ResourcesObj resourceObj;
                if (ResourcesObjDic.TryGetValue(obj.name, out resourceObj))
                {
                    resObj = resourceObj.obj;
                    resourceObj.CreateIns();
                }
                else
                {
                    mResLoader.Add2Load(obj.name, (b,res) =>
                    {
                        if(b)
                        {
                            resObj = res.Asset.As<GameObject>();
                            resObj.GetComponent<ObjMonoController>().SetsUid(obj.sUid);
                        }
                    });
                }

                CreateObj(resObj, sceneObj);
                sceneObj.status = SceneObjStatus.New;
            }
        }

        //private IEnumerator IELoad(ObjData obj)
        //{
        //    SceneObj sceneObj = new SceneObj(obj);
        //    sceneObj.status = SceneObjStatus.Loading;
        //    ActiveObjDic.Add(obj.sUid, sceneObj);
        //    GameObject resObj = null;
        //    ResourcesObj resourceObj;
        //    if (ResourcesObjDic.TryGetValue(obj.name, out resourceObj))
        //    {
        //        resObj = resourceObj.obj;
        //        resourceObj.CreateIns();
        //    }
        //    else
        //    {
        //        ResourceRequest request = Resources.LoadAsync<GameObject>(obj.name);
        //        yield return request;
        //        resObj = request.asset as GameObject;
        //    }

        //    CreateObj(resObj, sceneObj);
        //    sceneObj.status = SceneObjStatus.New;
        //}


        public void RefreshStatus()//刷新状态
        {
            DelKeysList.Clear();
            foreach (var pair in ActiveObjDic)
            {
                SceneObj sceneObj = pair.Value;
                if (sceneObj.status == SceneObjStatus.Old)
                {
                    DelKeysList.Add(pair.Key);
                }
                else if (sceneObj.status == SceneObjStatus.New)
                {
                    sceneObj.status = SceneObjStatus.Old;
                }
            }
            for (int i = 0; i < DelKeysList.Count; ++i)
            {
                MoveToInActive(ActiveObjDic[DelKeysList[i]].data);
            }
        }
    }

}