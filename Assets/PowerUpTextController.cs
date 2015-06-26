using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpTextController : MonoBehaviour {

	public Text PUText;

	private BonusCannedFoodType currentActivePowerUp;
	void Awake()
	{
		BonusPowerUpController.shared ().OnPowerUpStarted += HandleOnPowerUpStarted;
		BonusPowerUpController.shared ().OnPowerUpUpdated += HandleOnPowerUpUpdated;
		BonusPowerUpController.shared ().OnPowerUpEnded += HandleOnPowerUpEnded;

		BonusPowerUpController.shared().OnCanCanPowerUpTriggered += HandleOnCanCanPowerUpTriggered;
		BonusPowerUpController.shared().OnBadCanPowerUpTriggered += HandleOnBadCanPowerUpTriggered;
		BonusPowerUpController.shared().OnSlidePowerUpTriggered += HandleOnSlidePowerUpTriggered;

		BonusPowerUpController.shared().OnSwitchPlayModeTriggered += HandleOnSwitchPlayModeTriggered;
		BonusPowerUpController.shared().OnBonusGemTriggered += HandleOnBonusGemTriggered;
		BonusPowerUpController.shared().OnInstantCoinTriggered += HandleOnInstantCoinTriggered;
		BonusPowerUpController.shared().OnTimeMinusTriggered += HandleOnTimeMinusTriggered;
		BonusPowerUpController.shared().OnTimePlusTriggered += HandleOnTimePlusTriggered;

		currentActivePowerUp = BonusCannedFoodType.None;
		this.GetComponent<CanvasGroup> ().alpha = 0;
	}




	private IEnumerator FadeOutCoroutine ()
	{
		yield return new WaitForSeconds (1);

		float t = 1.0f;
		while (t>0.0f) {
			yield return new WaitForEndOfFrame ();
			t = Mathf.Clamp01 (t - Time.deltaTime);
			this.GetComponent<CanvasGroup>().alpha = t;
		}
	}

	void InitPowerUpText()
	{
		this.GetComponent<CanvasGroup>().alpha = 1;
		StopAllCoroutines ();
	}

	void HandleOnTimePlusTriggered (GameObject sender, int amount)
	{
		InitPowerUpText ();
		PUText.text = "TIME BONUS +"+amount+" seconds";
		StartCoroutine (FadeOutCoroutine());
	}

	void HandleOnTimeMinusTriggered (GameObject sender, int amount)
	{
		InitPowerUpText ();
		PUText.text = "TIME DECREASED "+amount+" seconds";
		StartCoroutine (FadeOutCoroutine());
	}

	void HandleOnInstantCoinTriggered (GameObject sender, int amount)
	{
		InitPowerUpText ();
		PUText.text = "You got "+amount+" x32 Can Bonus";
		StartCoroutine (FadeOutCoroutine());
	}

	void HandleOnBonusGemTriggered (GameObject sender, int amount)
	{
		InitPowerUpText ();
		PUText.text = "You got Extra "+amount+" GEM Bonus";
		StartCoroutine (FadeOutCoroutine());
	}

	void HandleOnSwitchPlayModeTriggered (GameObject sender, int amount)
	{
		InitPowerUpText ();
		PUText.text = "PLAY MODE ARE CHANGING BRO";
		StartCoroutine (FadeOutCoroutine());
	}

	void HandleOnBadCanPowerUpTriggered (GameObject sender, int amount)
	{
		currentActivePowerUp = BonusCannedFoodType.BadCan;
	}

	void HandleOnCanCanPowerUpTriggered (GameObject sender, int amount)
	{
		currentActivePowerUp = BonusCannedFoodType.CanCan;
	}
	void HandleOnSlidePowerUpTriggered (GameObject sender, int amount)
	{
		currentActivePowerUp = BonusCannedFoodType.TapToSlide;
	}





	void HandleOnPowerUpStarted (GameObject sender, float timer)
	{
		StopAllCoroutines();
		PowerUpSetText (currentActivePowerUp, timer);
	}

	void HandleOnPowerUpUpdated (GameObject sender, float timer)
	{
		PowerUpSetText (currentActivePowerUp, timer);
	}


	void HandleOnPowerUpEnded (GameObject sender, float timer)
	{
		StopAllCoroutines();
		PowerUpSetText (currentActivePowerUp, timer);
		currentActivePowerUp = BonusCannedFoodType.None;
		StartCoroutine (FadeOutCoroutine());
	}


	void PowerUpSetText(BonusCannedFoodType PU,float timer)
	{
		string text = "";
			switch (PU) {
		case BonusCannedFoodType.CanCan : text= "Double can Power up in " ; break;
		case BonusCannedFoodType.BadCan : text= "Defect can produced for " ; break;
		case BonusCannedFoodType.TapToSlide : text= "Concentrate Power up in " ; break;
		}
		PUText.text = text + timer.ToString("0.000") + " s";
		this.GetComponent<CanvasGroup>().alpha = 1;
	}







}
