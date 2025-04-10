using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace RpgGame
{
    public class TransFormModel : AbstractModel, ICanGetDataFactory
    {
        //id,transformData
        private Dictionary<int, TransformData> transform;

        protected override void OnInit()
        {
            transform = new Dictionary<int, TransformData>();
            if(transform.TryAdd(0, this.GetDataFactory().Get_TransformData()))
            {
                Debug.Log("fail to init model : " + GetType().Name);
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
            }
            else
            {
                Debug.Log("Can not find transformData : " + id);
            }
        }

        public void ChangePosition(int id, Vector3 value)
        {
            if(!transform.ContainsKey(id))
            {
                transform[id].position += value;
            }
            else
            {
                Debug.Log("Can not find transformData : " + id);
            }
        }

    }
}
