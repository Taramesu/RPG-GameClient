using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace RpgGame
{
    public class TransFormModel : AbstractModel, ICanGetModel
    {
        //id,transformData
        private Dictionary<int, TransformData> transforms;
        protected override void OnInit()
        {
            transforms = new Dictionary<int, TransformData>();
            if(!transforms.TryAdd(0, new TransformData()))
            {
                Debug.LogError("fail to init model : " + GetType().Name);
            }
        }

        public TransformData GetData(int id)
        {
            if (transforms.TryGetValue(id, out TransformData transformData)) 
            {
                return transformData;
            }
            else
            Debug.Log("Can not find transformData : " + id);
            return null;
        }

        public void ChangeData(int id, TransformData data)
        {
            if(transforms.ContainsKey(id)) 
            {
                transforms[id] = data;
                this.SendEvent(new TransFormDataChangeEvent { data = transforms[id] });
            }
            else
            {
                Debug.Log("Can not find transformData : " + id);
            }
        }

        public void ChangePosition(int id, Vector3 value)
        {
            if(transforms.ContainsKey(id))
            {
                transforms[id].position += value;
                this.SendEvent(new TransFormDataChangeEvent { data = transforms[id] });
            }
            else
            {
                Debug.Log("Can not find transformData : " + id);
            }
        }

    }
}
