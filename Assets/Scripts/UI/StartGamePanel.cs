using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class StartGamePanelData : UIPanelData
	{
	}
	public partial class StartGamePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as StartGamePanelData ?? new StartGamePanelData();
			// please add init code here
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
            Btn_NewGame.onClick.AddListener(NewGame);
            Btn_ContinueGame.onClick.AddListener(ContinueGame);
            Btn_Options.onClick.AddListener(Options);
            Btn_About.onClick.AddListener(About);
        }
		
		protected override void OnHide()
		{
            Btn_NewGame.onClick.RemoveListener(NewGame);
            Btn_ContinueGame.onClick.RemoveListener(ContinueGame);
            Btn_Options.onClick.RemoveListener(Options);
            Btn_About.onClick.RemoveListener(About);
        }
		
		protected override void OnClose()
		{
		}

		private void NewGame()
		{
			Debug.Log("start new game");
		}

		private void ContinueGame()
		{
            Debug.Log("continue game");
        }

		private void Options()
		{
            Debug.Log("options");
        }

		private void About()
		{
            Debug.Log("there is nothing bc the author is lazy");
        }
	}
}
