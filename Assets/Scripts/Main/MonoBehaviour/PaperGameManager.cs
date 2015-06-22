using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Artoncode.Core;

public class PaperGameManager : SingletonMonoBehaviour< PaperGameManager >
{
	public bool isDebug;
	public GamePlayModeType playMode;
	public List<SOColor> paperInGame;
	public List<SOUpgradableData> upgradeables;
	public List<int> comboLimit;
	private int _comboCounter = 0;
	private int _maxComboCounter = 0;
	private Dictionary<LevelMultiplierType, int> _collectedCannedFish = new Dictionary<LevelMultiplierType, int> ();
	private bool isPowerUpActivated;
	private float powerUpTimerCounter;
	private float powerUpTimerTotal = 5;
	private BonusCannedFoodType currentActivePowerUp;


	void Awake ()
	{
		playMode = GamePlayModeType.Say_The_Color;

		_collectedCannedFish [LevelMultiplierType.Negative1] = 0;
		_collectedCannedFish [LevelMultiplierType.Positive05] = 0;
		_collectedCannedFish [LevelMultiplierType.Positive1] = 0;
		_collectedCannedFish [LevelMultiplierType.Positive2] = 0;
		_collectedCannedFish [LevelMultiplierType.Positive4] = 0;
		_collectedCannedFish [LevelMultiplierType.Positive8] = 0;
		_collectedCannedFish [LevelMultiplierType.InstantBonus] = 0;
	
		isPowerUpActivated = false;
		currentActivePowerUp = BonusCannedFoodType.None;
		powerUpTimerCounter = 0;
	}

	void OnGUI ()
	{
		if (isDebug) {
			GUILayout.BeginVertical ("box");
			GUILayout.Label ("PlayMode : " + playMode);
			GUILayout.EndVertical ();
			GUILayout.BeginVertical ("box");
				
			GUILayout.Label ("Actived PU : "+ currentActivePowerUp.ToString() );
			GUILayout.Label ("Timer : " + powerUpTimerCounter );

			GUILayout.Label ("Canned " + LevelMultiplierType.Negative1.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.Negative1]);
			GUILayout.Label ("Canned " + LevelMultiplierType.Positive05.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.Positive05]);
			GUILayout.Label ("Canned " + LevelMultiplierType.Positive1.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.Positive1]);
			GUILayout.Label ("Canned " + LevelMultiplierType.Positive2.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.Positive2]);
			GUILayout.Label ("Canned " + LevelMultiplierType.Positive4.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.Positive4]);
			GUILayout.Label ("Canned " + LevelMultiplierType.Positive4.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.Positive8]);
			GUILayout.Label ("Canned " + LevelMultiplierType.InstantBonus.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.InstantBonus]);
			GUILayout.Label ("Combo : " + _comboCounter);
			GUILayout.Label ("Max Combo : " + _maxComboCounter);
			GUILayout.EndVertical ();
		}
	}

	public void TriggerPlayMode ()
	{
		switch (playMode) {
		case GamePlayModeType.Say_The_Color:
			playMode = GamePlayModeType.Say_The_Text;
			break;
		case GamePlayModeType.Say_The_Text:
			playMode = GamePlayModeType.Say_The_Color;
			break;
		} 
	}

	public void DoCorrect ()
	{
		int i = comboLimit.Count - 1;
		for (i = 0; i < comboLimit.Count-1; i++) {
			if (_comboCounter < comboLimit [i])
				break;
		}


		int idx = Mathf.Clamp (i, 0, comboLimit.Count - 1);
		LevelMultiplierType canMultiplier = (LevelMultiplierType)LevelMultiplierType.Positive1 + idx;



		//POWER UP ------------------------------------------------------------------------------------------------------------------------

		//POWER UP BAD CAN
		CheckBadCan (ref canMultiplier);

		//POWER UP CAN CAN
		StartCoroutine (CheckCanCan (canMultiplier));

		_collectedCannedFish [canMultiplier]++;
		CannedFoodMachineController.shared ().CreateCan (canMultiplier);

		 




		//POWER UP ------------------------------------------------------------------------------------------------------------------------

		_comboCounter++;
		if (_comboCounter > _maxComboCounter) {
			_maxComboCounter = _comboCounter;
		}
	}

	public void DoMistake ()
	{
		_comboCounter = 0;
		_collectedCannedFish [(LevelMultiplierType)LevelMultiplierType.Negative1]++;
		CannedFoodMachineController.shared ().CreateCan (LevelMultiplierType.Negative1);
	}

	private void CheckBadCan (ref LevelMultiplierType canMultiplier)
	{
		if (currentActivePowerUp == BonusCannedFoodType.BadCan) {
			canMultiplier = LevelMultiplierType.Positive05;
		} 
	}

	private IEnumerator CheckCanCan (LevelMultiplierType canMultiplier)
	{
		yield return new WaitForSeconds (0.2f);
		if (currentActivePowerUp == BonusCannedFoodType.CanCan) {
			_collectedCannedFish [canMultiplier]++;
			CannedFoodMachineController.shared ().CreateCan (canMultiplier);
		} 
	}

	public void TriggerPowerUp (BonusCannedFoodType powerUp)
	{
		switch (powerUp) {
			case BonusCannedFoodType.TimePlus:
			{
				break;
			}
			case BonusCannedFoodType.TimeMinus:
			{
				break;
			}
			case BonusCannedFoodType.BonusGem:
			{
				break;
			}
			case BonusCannedFoodType.InstantCoin:
			{
				break;
			}
			case BonusCannedFoodType.SwitchPlayMode:
			{
				TriggerPlayMode ();
				break;
			}
			case BonusCannedFoodType.TapToSlide:
			{
				isPowerUpActivated = true;
				powerUpTimerTotal = 5;
				powerUpTimerCounter = powerUpTimerTotal;
				currentActivePowerUp = BonusCannedFoodType.TapToSlide;
				break;
			}
			case BonusCannedFoodType.BadCan:
			{
				isPowerUpActivated = true;
				powerUpTimerTotal = 5;
				powerUpTimerCounter = powerUpTimerTotal;
				currentActivePowerUp = BonusCannedFoodType.BadCan;
				break;
			}
			case BonusCannedFoodType.CanCan:
			{
				isPowerUpActivated = true;
				powerUpTimerTotal = 5;
				powerUpTimerCounter = powerUpTimerTotal;
				currentActivePowerUp = BonusCannedFoodType.CanCan;
				break;
			}
		}
	}

	public void Update ()
	{
		if (isPowerUpActivated) {

			powerUpTimerCounter-=Time.deltaTime;

			if (powerUpTimerCounter <= 0) {
				powerUpTimerCounter = 0;
				currentActivePowerUp = BonusCannedFoodType.None;
				isPowerUpActivated= false;	
			}
		}
	}

	public bool isActivatedPowerUp(BonusCannedFoodType type)
	{
		return (currentActivePowerUp == type);
	}

}
