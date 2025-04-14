//using QFramework;
//using System.Collections.Generic;
//using UnityEngine;

//namespace RpgGame
//{
//    public class TransFormModel : AbstractModel, ICanGetModel
//    {
//        id,transformData
//        private Dictionary<int, List<TransformData>> datas;
//        protected override void OnInit()
//        {
//            datas = new Dictionary<int, List<TransformData>>();
//        }

//        public TransformData GetData(int typeId, int id)
//        {
//            if (datas.TryGetValue(typeId, out List<TransformData> transformDatas))
//            {
//                if (transformDatas)
//            }
//            else
//                Debug.Log("Can not find transformData : " + id);
//            return null;
//        }

//        public void ChangeData(int id, TransformData data)
//        {
//            if (datas.ContainsKey(id))
//            {
//                datas[id] = data;
//                this.SendEvent(new TransFormDataChangeEvent { data = datas[id] });
//            }
//            else
//            {
//                Debug.Log("Can not find transformData : " + id);
//            }
//        }

//        public void ChangePosition(int id, Vector3 value)
//        {
//            if (datas.ContainsKey(id))
//            {
//                datas[id].position += value;
//                this.SendEvent(new TransFormDataChangeEvent { data = datas[id] });
//            }
//            else
//            {
//                Debug.Log("Can not find transformData : " + id);
//            }
//        }

//        / <summary>
//        / ·µ»Øid
//        / </summary>
//        / <param name = "id" ></ param >
//        / < param name="data"></param>
//        / <returns></returns>
//        public int AddData(TransformData data)
//        {
//            {
//                if (datas.Add(data))
//                {
//                    this.SendEvent(new TransFormDataChangeEvent { data = datas[id] });
//                }
//                else
//                {
//                    Debug.Log($"The id:{id} add transform data fail!");
//                }
//            }
//        }

//    }
//}
