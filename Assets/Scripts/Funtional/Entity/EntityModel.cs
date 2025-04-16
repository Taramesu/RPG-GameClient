using QFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RpgGame
{
    public class EntityModel : AbstractModel
    {
        private Dictionary<int, List<EntityData>> dataDic;
        protected override void OnInit()
        {
            if(dataDic == null)
            {
                dataDic = new Dictionary<int, List<EntityData>>();
            }
        }

        public EntityData GetData(int typeId, int id)
        {
            if(dataDic.TryGetValue(typeId, out var datas))
            {
                if(id < datas.Count)
                {
                    return datas[id];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Debug.Log("Can not find EntityData: type." + id);
                return null;
            }
        }

        public bool TryAddEntity(int typeId, EntityData data, out int id)
        {
            if(dataDic.TryGetValue(typeId, out var datas))
            {
                if (TryGetDisableEntity(typeId, out int availableId))
                {
                    datas[availableId] = data;
                    id = availableId;
                    return true;
                }
                else
                {
                    datas.Add(data);
                    id = datas.Count - 1;
                    return true;
                }
            }
            else
            {
                if (dataDic.TryAdd(typeId, new List<EntityData>()))
                {
                    dataDic[typeId].Add(data);
                    id = dataDic.Count - 1;
                    return true;
                }
                else
                {
                    id = -1;
                    return false;
                }
            }
        }

        public bool UpdateData(int typeId, int id, EntityData data)
        {
            var oldData = GetData(typeId, id);
            if(oldData == null) return false;
            oldData = data;
            this.SendEvent(new EntityDataUpdateEvent() { typeId = typeId, id = id, data = oldData });
            return true;
        }

        public bool UpdatePosition(int typeId, int id, Vector3 value)
        {
            var data = GetData(typeId, id);
            if(data == null) return false;
            data.transform.position += value;
            this.SendEvent(new EntityPositionUpdateEvent() { typeId = typeId, id = id,sUid = data.sUid, position = data.transform.position });
            return true;
        }

        private bool TryGetDisableEntity(int typeId, out int id)
        {
            if(dataDic.TryGetValue(typeId, out var datas))
            {
                var result = datas.FirstOrDefault(x => x.enable == false);
                if (result == null)
                {
                    id = -1;
                    return false;
                }
                id = result.id;
                return true;
            }
            else
            {
                id = -1;
                return false;
            }
        }
    }
}