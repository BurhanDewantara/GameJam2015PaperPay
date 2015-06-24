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

	public float GAMETIME;

	private int _comboCounter = 0;
	private int _maxComboCounter = 0;
	private Dictionary<LevelMultiplierType, int> _collectedCannedFish = new Dictionary<LevelMultiplierType, int> ();
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
	
	}


	void Start()
	{
		GAMETIME = UpgradableDataController.shared ().GetDataValue (UpgradableType.PermanentTime);
		SetGameTime (GAMETIME);
		InitPowerUpHandler ();
	}

	void SetGameTime(float time)
	{
		TimerController.shared ().SetTime (time);
		TimerController.shared ().StartTime ();
	}

	void InitPowerUpHandler()
	{
		BonusPowerUpController.shared().OnSwitchPlayModeTriggered += HandleOnSwitchPlayModeTriggered;
		BonusPowerUpController.shared().OnBadCanPowerUpTriggered += HandleOnBadCanPowerUpTriggered;
		BonusPowerUpController.shared().OnCanCanPowerUpTriggered += HandleOnCanCanPowerUpTriggered;
		BonusPowerUpController.shared().OnPowerUpEnded += HandleOnPowerUpEnded;

	}

	void HandleOnPowerUpEnded (GameObject sender, float timer)
	{
		currentActivePowerUp = BonusCannedFoodType.None;
	}

	void HandleOnBadCanPowerUpTriggered (GameObject sender)
	{
		currentActivePowerUp = BonusCannedFoodType.BadCan;
	}
	void HandleOnCanCanPowerUpTriggered (GameObject sender)
	{
		currentActivePowerUp = BonusCannedFoodType.CanCan;
	}




	#region POWER UPS HANDLER
	void HandleOnSwitchPlayModeTriggered (GameObject sender)
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

	#endregion




	void OnGUI ()
	{
		if (isDebug) {
			GUILayout.BeginVertical ("box");
			GUILayout.Label ("PlayMode : " + playMode);
			GUILayout.EndVertical ();
			GUILayout.BeginVertical ("box");
				
			GUILayout.Label ("Canned " + LevelMultiplierType.Negative1.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.Negative1]);
			GUILayout.Label ("Canned " + LevelMultiplierType.Positive05.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.Positive05]);
			GUILayout.Label ("Canned " + LevelMultiplierType.Positive1.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.Positive1]);
			GUILayout.Label ("Canned " + LevelMultiplierType.Positive2.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.Positive2]);
			GUILayout.Label ("Canned " + LevelMultiplierType.Positive4.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.Positive4]);
			GUILayout.Label ("Canned " + LevelMultiplierType.Positive8.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.Positive8]);
			GUILayout.Label ("Canned " + LevelMultiplierType.InstantBonus.ToString () + " : " + _collectedCannedFish [LevelMultiplierType.InstantBonus]);
			GUILayout.Label ("Combo : " + _comboCounter);
			GUILayout.Label ("Max Combo : " + _maxComboCounter);
			GUILayout.EndVertical ();
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




}
