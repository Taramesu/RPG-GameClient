using QFramework;
using System.Collections.Generic;

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
        }


    }
}