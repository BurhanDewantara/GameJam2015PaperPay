using UnityEngine;
using System.Collections;
using Artoncode.Core;

public class BonusPowerUpController : SingletonMonoBehaviour<BonusPowerUpController> {

	public delegate void BonusPowerUpControllerDelegate(GameObject sender);
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




	public void TriggerPowerUp (BonusCannedFoodType powerUp)
	{
		switch (powerUp) {
		case BonusCannedFoodType.TimePlus:
		{
			if(OnTimePlusTriggered!=null)
				OnTimePlusTriggered(this.gameObject);
			break;
		}
		case BonusCannedFoodType.TimeMinus:
		{
			if(OnTimeMinusTriggered!=null)
				OnTimeMinusTriggered(this.gameObject);
			break;
		}
		case BonusCannedFoodType.BonusGem:
		{
			if(OnBonusGemTriggered!=null)
				OnBonusGemTriggered(this.gameObject);
			break;
		}
		case BonusCannedFoodType.InstantCoin:
		{
			if(OnInstantCoinTriggered!=null)
				OnInstantCoinTriggered(this.gameObject);
			break;
		}
		case BonusCannedFoodType.SwitchPlayMode:
		{
			if(OnSwitchPlayModeTriggered!=null)
				OnSwitchPlayModeTriggered(this.gameObject);
			break;
		}
		case BonusCannedFoodType.TapToSlide:
		{
			isAnyPowerUpActivated = true;
			powerUpTimerTotal = 5;
			powerUpTimerCounter = powerUpTimerTotal;
			currentActivePowerUp = BonusCannedFoodType.TapToSlide;
			if(OnPowerUpStarted !=null)
				OnPowerUpStarted(this.gameObject,powerUpTimerTotal);

			if(OnSlidePowerUpTriggered !=null)
				OnSlidePowerUpTriggered(this.gameObject);

			break;
		}
		case BonusCannedFoodType.BadCan:
		{
			isAnyPowerUpActivated = true;
			powerUpTimerTotal = 5;
			powerUpTimerCounter = powerUpTimerTotal;
			currentActivePowerUp = BonusCannedFoodType.BadCan;
			if(OnPowerUpStarted !=null)
				OnPowerUpStarted(this.gameObject,powerUpTimerTotal);
			if(OnBadCanPowerUpTriggered !=null)
				OnBadCanPowerUpTriggered(this.gameObject);

			break;
		}
		case BonusCannedFoodType.CanCan:
		{

			isAnyPowerUpActivated = true;
			powerUpTimerTotal = 5;
			powerUpTimerCounter = powerUpTimerTotal;
			currentActivePowerUp = BonusCannedFoodType.CanCan;
			if(OnPowerUpStarted !=null)
				OnPowerUpStarted(this.gameObject,powerUpTimerTotal);
			if(OnCanCanPowerUpTriggered !=null)
				OnCanCanPowerUpTriggered(this.gameObject);
			break;
		}
		}
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

