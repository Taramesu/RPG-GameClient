using Cysharp.Threading.Tasks;
using QFramework;
using System;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

namespace RpgGame
{
    public class EntitySystem : AbstractSystem, ICanGetModel
    {
        private ResLoader mResLoader;
        protected override void OnInit()
        {
            mResLoader = ResLoader.Allocate();
        }

        ///// <summary>
        ///// 实体生成
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="transformData"></param>
        ///// <returns></returns>
        //public bool EntityGenerate2(int typeId, TransformData transformData = default)
        //{
        //    var config = EntityTable.GetConfigById(typeId);
        //    if (config == null) return false;
            
        //    //添加EntityData
        //    var eModel = this.GetModel<EntityModel>();
        //    if (eModel == null) return false;
        //    var data = new EntityData(typeId, transformData);
        //    eModel.TryAddEntity(typeId, data, out int id);

        //    var oModel = this.GetModel<ObjModel>();
        //    if (oModel == null) return false;
        //    oModel.TryAddData(data);

        //    ResourcesManager.Instance.Load(data);
        //    this.SendEvent(new EntityGenerateEvent() { data = data });

        //    return true;
        //}

        /// <summary>
        /// 实体生成
        /// </summary>
        /// <param name="typeId">类型id</param>
        /// <param name="transformData">位置旋转缩放数据</param>
        /// <returns></returns>
        public GameObject GenerateEntity(int typeId, TransformData transformData = default)
        {
            //添加EntityData
            var eModel = this.GetModel<EntityModel>();
            var data = new EntityData(typeId, transformData);
            //eModel.TryAddEntity(typeId, data, out int id);
            eModel.TryAddData(data);

            var oModel = this.GetModel<ObjModel>();
            oModel.TryAddData(data);

            //GameObject go = null;
            //mResLoader.Add2Load<GameObject>(data.name, (b, res) =>
            //{
            //    var prefab = res.Asset.As<GameObject>();
            //    go = Pool.Instance.CreateObject(data.name, prefab, transformData.position, transformData.rotation);
            //    go.GetComponent<ObjMonoController>().SetsUid(data.sUid);
            //});

            //mResLoader.LoadAsync();

            var prefab = mResLoader.LoadSync<GameObject>(data.name);
            var go = Pool.Instance.CreateObject(data.name, prefab, transformData.position, transformData.rotation);
            go.GetComponent<ObjMonoController>().SetsUid(data.sUid);

            return go;
        }

        /// <summary>
        /// 回收实体
        /// </summary>
        /// <param name="go">物体</param>
        /// <param name="delay">延迟时间(ms)</param>
        public async void CollectEntity(GameObject go, int delay = 0)
        {
            await UniTask.Delay(delay);
            var controller = go.GetComponent<ObjMonoController>();
            var sUid = controller.GetsUid();
            //var sUid = go.GetComponent<ObjMonoController>().GetsUid();
            this.GetModel<ObjModel>().RemoveData(sUid);
            this.GetModel<EntityModel>().RemoveData(sUid);
            Pool.Instance.CollectObject(go);
        }

        protected override void OnDeinit()
        {
            base.OnDeinit();
            mResLoader.Recycle2Cache();
            mResLoader = null;
        }
    }
}