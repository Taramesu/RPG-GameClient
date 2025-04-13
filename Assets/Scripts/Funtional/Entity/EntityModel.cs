using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace RpgGame
{
    public class EntityModel : AbstractModel
    {
        private Dictionary<int, EntityData> datas;
        protected override void OnInit()
        {
            if(datas == null)
            {
                datas = new Dictionary<int, EntityData>();
            }
            InitEntityData();
        }

        private void InitEntityData()
        {
            datas.Add(0, new EntityData(0));
        }

        public EntityData GetData(int id)
        {
            if(datas.TryGetValue(id, out var data))
            {
                return data;
            }
            else
            {
                Debug.Log("Can not find EntityData: No." + id);
                return null;
            }
        }
    }
}