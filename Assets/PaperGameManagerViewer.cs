using UnityEngine;
using System.Collections;

public class PaperGameManagerViewer : MonoBehaviour
{
	public bool isDebug;
	private bool showHelper;

	float height = 40;
	float width = 100;
	float padw = 20;

	void OnGUI ()
	{
		if (isDebug) 
		{
			if (showHelper = GUI.Toggle (new Rect (0, Screen.height - 20, 20, 20), showHelper, "")) 
			{
				if (GUI.Button (new Rect (padw + width * 0, Screen.height-height * 1, width, height), "PlayMode")) {
					PaperGameManager.shared ().TriggerPowerUp (BonusCannedFoodType.SwitchPlayMode);
				}
				
				if (GUI.Button (new Rect (padw + width * 1, Screen.height-height * 1, width, height), "BadCan")) {
					PaperGameManager.shared ().TriggerPowerUp (BonusCannedFoodType.BadCan);
				}
				
				if (GUI.Button (new Rect (padw + width * 2, Screen.height-height * 1, width, height), "CanCan")) {
					PaperGameManager.shared ().TriggerPowerUp (BonusCannedFoodType.CanCan);
				}

				if (GUI.Button (new Rect (padw + width * 0, Screen.height-height * 2, width, height), "Instant Coin")) {
					PaperGameManager.shared ().TriggerPowerUp (BonusCannedFoodType.InstantCoin);
				}

				if (GUI.Button (new Rect (padw + width * 1, Screen.height-height * 2, width, height), "TapToSlide")) {
					PaperGameManager.shared ().TriggerPowerUp (BonusCannedFoodType.TapToSlide);
				}
				if (GUI.Button (new Rect (padw + width * 2, Screen.height-height * 2, width, height), "TimeMinus")) {
					PaperGameManager.shared ().TriggerPowerUp (BonusCannedFoodType.TimeMinus);
				}
				if (GUI.Button (new Rect (padw + width * 0, Screen.height-height * 3, width, height), "TimePlus")) {
					PaperGameManager.shared ().TriggerPowerUp (BonusCannedFoodType.TimePlus);
				}
				if (GUI.Button (new Rect (padw + width * 1, Screen.height-height * 3, width, height), "BonusGem")) {
					PaperGameManager.shared ().TriggerPowerUp (BonusCannedFoodType.BonusGem);
				}
			}
		}
	}


}
