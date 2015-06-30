using UnityEngine;
using System.Collections;
using Artoncode.Core;

public class BonusPowerUpController : SingletonMonoBehaviour<BonusPowerUpController> {

	public delegate void BonusPowerUpControllerDelegate(GameObject sender,int amount);
	public delegate void BonusPowerUpControllerDelegateWithTimer(GameObject sender, float timer);


	public event BonusPowerUpControllerDelegateWithTimer OnPowerUpStarted;
	public event BonusPowerUpControllerDelegateWithTimer OnPowerUpUpdated;
	public event BonusPowerUpControllerDelegateWithTimer OnPowerUpEnded;


	public event BonusPowerUpControllerDelegate OnTimePlusTriggered;
	public event BonusPowerUpControllerDelegate OnTimeMinusTriggered;
	public event BonusPowerUpControllerDelegate OnBonusGemTriggered;
	public event BonusPowerUpControllerDelegate OnInstantCoinTriggered;
	public event BonusPowerUpControllerDelegate OnSwitchPlayModeTriggered;


	public event BonusPowerUpControllerDelegate OnBadCanPowerUpTriggered;
	public event BonusPowerUpControllerDelegate OnCanCanPowerUpTriggered;
	public event BonusPowerUpControllerDelegate OnSlidePowerUpTriggered;



	public bool isAnyPowerUpActivated;
	private float powerUpTimerCounter;
	private float powerUpTimerTotal = 5;

	public BonusCannedFoodType currentActivePowerUp;

	void Awake()
	{
		isAnyPowerUpActivated = false;
		currentActivePowerUp = BonusCannedFoodType.None;
		powerUpTimerCounter = 0;
	}

	//POWER UPS

	//POWERUPS




	public void TriggerPowerUp (BonusCannedFoodType powerUp, int amount = 5, bool isDoubled = false)
	{
		string audioStr= "canbonus";


		switch (powerUp) {
		case BonusCannedFoodType.TimePlus:
		{
			if(OnTimePlusTriggered!=null)
				OnTimePlusTriggered(this.gameObject,(isDoubled ? amount*2 : amount));
			break;
		}
	
		case BonusCannedFoodType.BonusGem:
		{
			if(OnBonusGemTriggered!=null)
				OnBonusGemTriggered(this.gameObject,(isDoubled ? amount*2 : amount));
			break;
		}
		case BonusCannedFoodType.InstantCoin:
		{
			if(OnInstantCoinTriggered!=null)
				OnInstantCoinTriggered(this.gameObject,(isDoubled ? amount*2 : amount));
			break;
		}
	
		case BonusCannedFoodType.TapToSlide:
		{
			isAnyPowerUpActivated = true;
			powerUpTimerTotal = isDoubled? amount*2 : amount;
			powerUpTimerCounter = powerUpTimerTotal;
			currentActivePowerUp = BonusCannedFoodType.TapToSlide;
			if(OnPowerUpStarted !=null)
				OnPowerUpStarted(this.gameObject,powerUpTimerTotal);

			if(OnSlidePowerUpTriggered !=null)
				OnSlidePowerUpTriggered(this.gameObject,(isDoubled ? amount*2 : amount));

			break;
		}
		case BonusCannedFoodType.BadCan:
		{
			audioStr= "canbad";
			isAnyPowerUpActivated = true;
			powerUpTimerTotal = 5;
			powerUpTimerCounter = powerUpTimerTotal;
			currentActivePowerUp = BonusCannedFoodType.BadCan;
			if(OnPowerUpStarted !=null)
				OnPowerUpStarted(this.gameObject,powerUpTimerTotal);
			if(OnBadCanPowerUpTriggered !=null)
				OnBadCanPowerUpTriggered(this.gameObject,amount);

			break;
		}
		case BonusCannedFoodType.SwitchPlayMode:
		{
			audioStr= "canbad";
			if(OnSwitchPlayModeTriggered!=null)
				OnSwitchPlayModeTriggered(this.gameObject,amount);
			break;
		}
		case BonusCannedFoodType.TimeMinus:
		{
			audioStr= "canbad";
			if(OnTimeMinusTriggered!=null)
				OnTimeMinusTriggered(this.gameObject,amount);
			break;
		}
		case BonusCannedFoodType.CanCan:
		{

			isAnyPowerUpActivated = true;
			powerUpTimerTotal = isDoubled? amount*2 : amount;
			powerUpTimerCounter = powerUpTimerTotal;
			currentActivePowerUp = BonusCannedFoodType.CanCan;
			if(OnPowerUpStarted !=null)
				OnPowerUpStarted(this.gameObject,powerUpTimerTotal);
			if(OnCanCanPowerUpTriggered !=null)
				OnCanCanPowerUpTriggered(this.gameObject,(isDoubled ? amount*2 : amount));
			break;
		}
		}


		AudioController.shared ().PlayAudio (audioStr);

	}




	public void Update ()
	{
		if (isAnyPowerUpActivated) {

			powerUpTimerCounter-=Time.deltaTime;

			if(OnPowerUpUpdated !=null)
				OnPowerUpUpdated(this.gameObject,powerUpTimerCounter);

			if (powerUpTimerCounter <= 0) {
				powerUpTimerCounter = 0;
				currentActivePowerUp = BonusCannedFoodType.None;
				isAnyPowerUpActivated= false;	
				if(OnPowerUpUpdated !=null)
					OnPowerUpUpdated(this.gameObject,powerUpTimerCounter);
				if(OnPowerUpEnded !=null)
					OnPowerUpEnded(this.gameObject,powerUpTimerCounter);
			}
		}
	}
	
	public bool isActivatedPowerUp(BonusCannedFoodType type)
	{
		return (currentActivePowerUp == type);
	}

}

