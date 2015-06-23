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

	void HandleOnTimePlusTriggered (GameObject sender)
	{
		HandleOnPowerUpStarted (null);
		PUText.text = "TIME BONUS +5 seconds";
		StartCoroutine (FadeOutCoroutine());
	}

	void HandleOnTimeMinusTriggered (GameObject sender)
	{
		HandleOnPowerUpStarted (null);
		PUText.text = "TIME DECREASED 5 seconds";
		StartCoroutine (FadeOutCoroutine());
	}

	void HandleOnInstantCoinTriggered (GameObject sender)
	{
		HandleOnPowerUpStarted (null);
		PUText.text = "You got INSTANT COIN Bonus";
		StartCoroutine (FadeOutCoroutine());
	}

	void HandleOnBonusGemTriggered (GameObject sender)
	{
		HandleOnPowerUpStarted (null);
		PUText.text = "You got Extra GEM Bonus";
		StartCoroutine (FadeOutCoroutine());
	}

	void HandleOnSwitchPlayModeTriggered (GameObject sender)
	{
		HandleOnPowerUpStarted (null);
		PUText.text = "PLAY MODE ARE CHANGING BRO";
		StartCoroutine (FadeOutCoroutine());
	}

	void HandleOnBadCanPowerUpTriggered (GameObject sender)
	{
		currentActivePowerUp = BonusCannedFoodType.BadCan;
	}

	void HandleOnCanCanPowerUpTriggered (GameObject sender)
	{
		currentActivePowerUp = BonusCannedFoodType.CanCan;
	}
	void HandleOnSlidePowerUpTriggered (GameObject sender)
	{
		currentActivePowerUp = BonusCannedFoodType.TapToSlide;
	}

	void HandleOnPowerUpStarted (GameObject sender)
	{
		StopAllCoroutines();
		this.GetComponent<CanvasGroup> ().alpha = 1;
	}

	void HandleOnPowerUpUpdated (GameObject sender, float timer)
	{
		string text = "";
		switch (currentActivePowerUp) {
		case BonusCannedFoodType.CanCan : text= "CAN CAN POWER UP IN " ; break;
		case BonusCannedFoodType.BadCan : text= "BAN CAN POWER UP IN " ; break;
		case BonusCannedFoodType.TapToSlide : text= "TAP TO SLIDE IN " ; break;
		}
		PUText.text = text + timer.ToString("0.000") + " s";
		this.GetComponent<CanvasGroup>().alpha = 1;
	}


	void HandleOnPowerUpEnded (GameObject sender)
	{
		StopAllCoroutines();
		currentActivePowerUp = BonusCannedFoodType.None;
		StartCoroutine (FadeOutCoroutine());
	}
}
