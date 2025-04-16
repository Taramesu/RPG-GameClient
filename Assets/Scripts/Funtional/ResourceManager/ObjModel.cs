using QFramework;
using System.Collections.Generic;

namespace RpgGame
{
    public class ObjModel : AbstractModel
    {
        //sUid,objData
        private Dictionary<string, ObjData> datas;
        protected override void OnInit()
        {
            if (datas == null ) 
            {
                datas = new Dictionary<string, ObjData>();
            }
        }

        public ObjData GetData( string sUid ) 
        {
            if(datas.TryGetValue(sUid, out var data))
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public bool TryAddData(ObjData objData)
        {
            if(datas.TryAdd(objData.sUid, objData))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}