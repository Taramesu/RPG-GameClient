using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace RpgGame
{
    public class TransFormModel : AbstractModel
    {
        //id,transformData
        private Dictionary<int, TransformData> transform;

        protected override void OnInit()
        {
            transform = new Dictionary<int, TransformData>();
            if(!transform.TryAdd(0, new TransformData()))
            {
                Debug.LogError("fail to init model : " + GetType().Name);
            }
        }

        public TransformData GetData(int id)
        {
            if (transform.TryGetValue(id, out TransformData transformData)) 
            {
                return transformData;
            }
            else
            Debug.Log("Can not find transformData : " + id);
            return null;
        }

        public void ChangeData(int id, TransformData data)
        {
            if(transform.ContainsKey(id)) 
            {
                transform[id] = data;
                this.SendEvent(new TransFormDataChangeEvent { data = transform[id] });
            }
            else
            {
                Debug.Log("Can not find transformData : " + id);
            }
        }

        public void ChangePosition(int id, Vector3 value)
        {
            if(transform.ContainsKey(id))
            {
                transform[id].position += value;
                this.SendEvent(new TransFormDataChangeEvent { data = transform[id] });
            }
            else
            {
                Debug.Log("Can not find transformData : " + id);
            }
        }

    }
}
