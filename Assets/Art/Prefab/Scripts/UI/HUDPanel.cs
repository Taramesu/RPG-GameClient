using UnityEngine;
using UnityEngine.UI;
using QFramework;
using RpgGame;

namespace RpgGame
{
    public class HUDPanelData : UIPanelData
    {
        public IArchitecture GetArchitecture()
        {
			return RpgGame.Interface;
        }
    }
    public partial class HUDPanel : UIPanel
	{
		private string sUid;
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HUDPanelData ?? new HUDPanelData();
			// please add init code here
			//this.RegisterEvent<EntityHpUpdateEvent>(OnHpUpdate);
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
            //this.UnRegisterEvent<EntityHpUpdateEvent>(OnHpUpdate);
        }

		private void OnHpUpdate(EntityHpUpdateEvent context)
		{
			
		}
	}
}
