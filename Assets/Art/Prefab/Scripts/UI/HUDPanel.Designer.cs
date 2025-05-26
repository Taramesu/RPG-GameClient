using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace RpgGame
{
	// Generate Id:4ff105b0-5297-4846-8a9b-0db5033f687f
	public partial class HUDPanel
	{
		public const string Name = "HUDPanel";
		
		[SerializeField]
		public UnityEngine.UI.Slider PlayerHP;
		[SerializeField]
		public UnityEngine.UI.Slider PlayerMP;
		
		private HUDPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			PlayerHP = null;
			PlayerMP = null;
			
			mData = null;
		}
		
		public HUDPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		HUDPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new HUDPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
