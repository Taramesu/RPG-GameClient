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
        /// ʵ������
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transformData"></param>
        /// <returns></returns>
        public bool EntityGenerate(int typeId, TransformData transformData = default)
        {
            var config = EntityTable.GetConfigById(typeId);
            if (config == null) return false;
            //var prefab = mResLoader.LoadSync<GameObject>(config.Name);
            

            //���EntityData
            var model = this.GetModel<EntityModel>();
            if (model == null) return false;
            var data = new EntityData(typeId, transformData);
            model.TryAddEntity(typeId, data, out int id);

            ResourcesManager.Instance.Load(data);
            //if (prefab == null) return false;

            //this.SendEvent(new EntityGenerateEvent { typeId = typeId, id = id, transformData = transformData, prefab = prefab });
            return true;
        }
    }
}