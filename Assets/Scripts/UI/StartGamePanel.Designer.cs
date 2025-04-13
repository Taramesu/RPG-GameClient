using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:e8aab389-1e93-462e-b72b-b1ecaede7e34
	public partial class StartGamePanel
	{
		public const string Name = "StartGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Button Btn_NewGame;
		[SerializeField]
		public UnityEngine.UI.Button Btn_ContinueGame;
		[SerializeField]
		public UnityEngine.UI.Button Btn_Options;
		[SerializeField]
		public UnityEngine.UI.Button Btn_About;
		
		private StartGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Btn_NewGame = null;
			Btn_ContinueGame = null;
			Btn_Options = null;
			Btn_About = null;
			
			mData = null;
		}
		
		public StartGamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		StartGamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new StartGamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
