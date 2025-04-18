using QFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RpgGame
{
    public class EntityModel : AbstractModel
    {
        //private Dictionary<int, List<EntityData>> dataDic;
        private Dictionary<string, EntityData> datas;
        protected override void OnInit()
        {
            if(datas == null)
            {
                datas = new();
            }
            //if(dataDic == null)
            //{
            //    dataDic = new Dictionary<int, List<EntityData>>();
            //}
        }

        public EntityData GetData(string sUid) 
        {
            if(datas.TryGetValue(sUid, out var data))
            {
                return data;
            }
            return null;
        }

        public EntityData GetData(int typeId)
        {
            return datas.Values.FirstOrDefault(x => x.typeId == typeId);
        }

        public List<EntityData> GetDatas(int typeId)
        {
            return datas.Values.Where(d => d.typeId == typeId).ToList();
        }

        public bool TryAddData(EntityData data)
        {
            if(datas.TryAdd(data.sUid, data))
            {
                this.SendEvent(new EntityGenerateEvent() { data = data });
                return true;
            }
            return false;
        }

        public bool UpdateData(EntityData data)
        {
            if(datas.ContainsKey(data.sUid))
            {
                datas[data.sUid] = data;
                return true;
            }
            if(datas.TryAdd(data.sUid, data))
            {
                return true;
            }
            return false;
        }

        public bool UpdatePosition(string sUid, Vector3 value)
        {
            if(datas.ContainsKey(sUid)) 
            {
                datas[sUid].transform.position += value;
                this.SendEvent(new EntityPositionUpdateEvent() { sUid = sUid, position = datas[sUid].transform.position });
                return true;
            }
            Debug.Log($"Can not find data with sUid:{sUid}");
            return false;
        }

        public bool RemoveData(string sUid)
        {
            if(datas.ContainsKey(sUid)) 
            {
                this.SendEvent(new EntityCollectEvent() { data = datas[sUid] });
                return datas.Remove(sUid);
            }
            return false;
        }

        //public EntityData GetData(int typeId, int id)
        //{
        //    if(dataDic.TryGetValue(typeId, out var datas))
        //    {
        //        if(id < datas.Count)
        //        {
        //            return datas[id];
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("Can not find EntityData: type." + id);
        //        return null;
        //    }
        //}

        //public bool TryAddEntity(int typeId, EntityData data, out int id)
        //{
        //    if(dataDic.TryGetValue(typeId, out var datas))
        //    {
        //        if (TryGetDisableEntity(typeId, out int availableId))
        //        {
        //            datas[availableId] = data;
        //            id = availableId;
        //            return true;
        //        }
        //        else
        //        {
        //            datas.Add(data);
        //            id = datas.Count - 1;
        //            //var OModel = this.GetModel<ObjModel>();
        //            //OModel.TryAddData(data);
        //            return true;
        //        }
        //    }
        //    else
        //    {
        //        if (dataDic.TryAdd(typeId, new List<EntityData>()))
        //        {
        //            dataDic[typeId].Add(data);
        //            id = dataDic.Count - 1;
        //            return true;
        //        }
        //        else
        //        {
        //            id = -1;
        //            return false;
        //        }
        //    }
        //}

        //public bool UpdateData(int typeId, int id, EntityData data)
        //{
        //    var oldData = GetData(typeId, id);
        //    if(oldData == null) return false;
        //    oldData = data;
        //    this.SendEvent(new EntityDataUpdateEvent() { typeId = typeId, id = id, data = oldData });
        //    return true;
        //}

        //public bool UpdatePosition(int typeId, int id, Vector3 value)
        //{
        //    var data = GetData(typeId, id);
        //    if(data == null) return false;
        //    data.transform.position += value;
        //    this.SendEvent(new EntityPositionUpdateEvent() { sUid = data.sUid, position = data.transform.position });
        //    return true;
        //}

        //public void RemoveData(int typeId, int id)
        //{
        //    if (!dataDic.ContainsKey(typeId)) return;
        //    var datas = dataDic[typeId];
        //    //if(datas.Contains()
        //}

        //private bool TryGetDisableEntity(int typeId, out int id)
        //{
        //    if(dataDic.TryGetValue(typeId, out var datas))
        //    {
        //        var result = datas.FirstOrDefault(x => x.enable == false);
        //        if (result == null)
        //        {
        //            id = -1;
        //            return false;
        //        }
        //        id = result.id;
        //        return true;
        //    }
        //    else
        //    {
        //        id = -1;
        //        return false;
        //    }
        //}
    }
}