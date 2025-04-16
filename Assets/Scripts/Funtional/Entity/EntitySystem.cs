using QFramework;
using System;
using System.Runtime.InteropServices;
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

        /// <summary>
        /// 实体生成
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transformData"></param>
        /// <returns></returns>
        public bool EntityGenerate(int typeId, TransformData transformData = default)
        {
            var config = EntityTable.GetConfigById(typeId);
            if (config == null) return false;
            
            //添加EntityData
            var eModel = this.GetModel<EntityModel>();
            if (eModel == null) return false;
            var data = new EntityData(typeId, transformData);
            eModel.TryAddEntity(typeId, data, out int id);

            var oModel = this.GetModel<ObjModel>();
            if (oModel == null) return false;
            oModel.TryAddData(data);

            ResourcesManager.Instance.Load(data);
            this.SendEvent(new EntityGenerateEvent() { data = data });

            return true;
        }
    }
}