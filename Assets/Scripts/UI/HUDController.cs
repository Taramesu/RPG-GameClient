using Autodesk.Fbx;
using Cysharp.Threading.Tasks;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace RpgGame
{
    public class HUDController : MonoBehaviour, IController
    {
        private string sUid;
        private float initHp;
        private float initMp;
        private Slider playerHp;
        private Slider playerMp;
        public IArchitecture GetArchitecture() => RpgGame.Interface;

        public void Start()
        {
            Init();
        }

        private async void Init()
        {
            await UniTask.WaitUntil(() => this.GetModel<EntityModel>().GetData(0) != null);
            var data = this.GetModel<EntityModel>().GetData(0);
            sUid = data.sUid;
            initHp = data.property.Hp;
            initMp = data.property.Mp;
            playerHp = transform.GetChild(0).GetComponent<Slider>();
            playerMp = transform.GetChild(1).GetComponent<Slider>();
            this.RegisterEvent<EntityHpUpdateEvent>(OnHpUdate);
            this.RegisterEvent<EntityMpUpdateEvent>(OnMpUdate);
        }

        public void OnDestroy()
        {
            this.UnRegisterEvent<EntityHpUpdateEvent>(OnHpUdate);
            this.UnRegisterEvent<EntityMpUpdateEvent>(OnMpUdate);
        }

        private void OnHpUdate(EntityHpUpdateEvent context)
        {
            if(context.sUid != sUid) return;
            playerHp.value = context.Hp/initHp;
        }

        private void OnMpUdate(EntityMpUpdateEvent context)
        {
            if (context.sUid != sUid) return;
            playerMp.value = context.Mp / initMp;
        }
    }
}