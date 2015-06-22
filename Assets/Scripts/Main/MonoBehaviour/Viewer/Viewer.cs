using UnityEngine;
using System.Collections;

public class Viewer : MonoBehaviour
{
	private bool showHelper = false;

	void OnGUI ()
	{
		if (showHelper = GUI.Toggle (new Rect(Screen.width-20,Screen.height-20,20,20),showHelper, "")) {
			addButtons ();
		}
	}
	
	void addButtons ()
	{
		GUILayout.Label (CurrencyManager.shared ().ToString ());
		GUILayout.Label (PlayerUpgradableDataManager.shared ().ToString ());
		GUILayout.Space (10);

		GUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button ("Reset Data")) {
				GameHelper.ResetData ();
			}
		}
		GUILayout.EndHorizontal ();


		GUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button ("Add Coins")) {
				GameHelper.GetCoins ();
			}
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button ("+ Time")) {
				GameHelper.Upgrade (UpgradableType.PermanentTime);
			}
			if (GUILayout.Button ("+ Multi")) {
				GameHelper.Upgrade (UpgradableType.CanMultiplier);
			}
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button ("+% Bonus")) {
				GameHelper.Upgrade (UpgradableType.ChancesBonusCan);
			}
			if (GUILayout.Button ("+% Gem")) {
				GameHelper.Upgrade (UpgradableType.ChancesBonusGem);
			}
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button ("+ Score")) {
				GameHelper.Upgrade (UpgradableType.ExtraTotalBonus);
			}
			if (GUILayout.Button ("+ Speed")) {
				GameHelper.Upgrade (UpgradableType.PaperSlideSpeed);
			}
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button ("- Mistake")) {
				GameHelper.Upgrade (UpgradableType.ReduceMistakePaperCost);
			}

			if (GUILayout.Button ("- UpgradeCost")) {
				GameHelper.Upgrade (UpgradableType.ReduceUpgradeCost);
			}
		}

		GUILayout.EndHorizontal ();

	}
}
