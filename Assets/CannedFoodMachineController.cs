using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Artoncode.Core;

public class CannedFoodMachineController : SingletonMonoBehaviour<CannedFoodMachineController> {

	public GameObject canContainer;
	public GameObject canPrefab;

	public List<StandardCannedFood> CannedFoodData;
	public List<BonusCannedFood> CannedFoodBonus;
	public List<InstantCannedFood> CannedFoodInstantBonus;


	public GameObject powerUpPopUpPrefab;
	private GameObject _powerUpPopUpObject;


	private List<GameObject> _cannedFoodObject;


	public void SetIsAccessible(bool val =false)
	{
		
		foreach (GameObject item in _cannedFoodObject) {
			item.GetComponent<CannedFoodContent>().SetButtonEnable(val);
		
		}
	}


	void Start()
	{
		_cannedFoodObject = new List<GameObject> ();

	}

	void MoveAllCan()
	{
		foreach (GameObject can in _cannedFoodObject) {
			can.GetComponent<CannedFoodContentViewer>().Slide();
		}
	}



	public void CreateCan(LevelMultiplierType levelMultiplierType,float gemChances = 0,float bonusChances = 0)
	{ 
		MoveAllCan ();
		GameObject obj = CreateCanObject (canPrefab);
		obj.GetComponent<RectTransform> ().SetParent (canContainer.GetComponent<RectTransform> (),false);
		obj.GetComponent<RectTransform> ().SetAsFirstSibling ();

		SOCannedFood CanFood = GetCannedFoodData (levelMultiplierType);

		if(isBonusCannedFoodIsEligibleToProduce ())
		{
			if (levelMultiplierType >= LevelMultiplierType.Positive1) 
			{
				//dapet gem
				if (isChancesHit (gemChances)) 
				{
					CanFood = GetInstantCannedFood(BonusCannedFoodType.BonusGem);
				} 
				// dapet normal powerup (with bad chances)
				else if (isChancesHit (bonusChances)) 
				{ 
					int randresult = Random.Range(0,8);
					CanFood = GetBonusCannedFood((BonusCannedFoodType)randresult);
				} 
			}
			else{
				//badcan
				if(isChancesHit(50))
				{
					CanFood = GetAnyNegativeOnlyCannedFood();
				}else if(isChancesHit(20))
				{
					CanFood = GetAnyPositiveOnlyCannedFood();
				}

			}

		}
		obj.GetComponent<CannedFoodContent> ().SetItem (new CannedFoodItem (CanFood));
		_cannedFoodObject.Add (obj);
	}


	public bool isBonusCannedFoodIsEligibleToProduce()
	{
		bool anyPowerUpCanExist = false;

		foreach (GameObject item in _cannedFoodObject) {
			if(anyPowerUpCanExist = item.GetComponent<CannedFoodContent> ().can.isPowerUp)
				break;
		}
		return !BonusPowerUpController.shared ().isAnyPowerUpActivated && !anyPowerUpCanExist;
	}


	GameObject CreateCanObject(GameObject canPrefab)
	{
		GameObject obj = Instantiate (canPrefab) as GameObject;
		obj.GetComponent<CannedFoodContentViewer>().OnCanDestroyed += HandleOnCanDestroyed;
		obj.GetComponent<CannedFoodContentViewer>().OnCanClicked += HandleOnCanClicked;
		return obj;
	}

	void HandleOnCanClicked (GameObject sender)
	{
		// DO SOMETHING! BONUS CAN!!!!
		SOCannedFood can = sender.GetComponent<CannedFoodContent> ().can.canFood;

		if (can is BonusCannedFood) 
		{
			CreatePowerUpPopUp(can as BonusCannedFood);
			TimerController.shared().StopTime();

		} 
		else if (can is InstantCannedFood)
		{
			AudioController.shared ().PlayAudio ("bonusgem");
			BonusCannedFoodType bonustype = (can as InstantCannedFood).bonusType;
			switch (bonustype) 
			{
				case BonusCannedFoodType.BonusGem : 
				BonusPowerUpController.shared().TriggerPowerUp((can as InstantCannedFood).bonusType,(can as InstantCannedFood).bonusAmount); 
				break;
			}
		}

		_cannedFoodObject.Remove (sender);
		Destroy (sender);
	}



	void CreatePowerUpPopUp(BonusCannedFood bonusCan)
	{
		if (_powerUpPopUpObject == null) {
			AudioController.shared ().SetMainAudioSoundVolume(0.3f);
			AudioController.shared ().PlayAudio ("popupbonus");
			_powerUpPopUpObject = Instantiate(powerUpPopUpPrefab) as GameObject;

			_powerUpPopUpObject.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>().parent.GetComponent<RectTransform>(),false);
			_powerUpPopUpObject.GetComponent<PowerUpPopUpController>().OnPayButtonClicked	 += HandlePOPOnPayButtonClicked;
			_powerUpPopUpObject.GetComponent<PowerUpPopUpController>().OnFreeButtonClicked	 += HandlePOPOnFreeButtonClicked;
			_powerUpPopUpObject.GetComponent<PowerUpPopUpController>().OnAcceptButtonClicked += HandlePOPOnAcceptButtonClicked;
			_powerUpPopUpObject.GetComponent<PowerUpPopUpController>().SetBonusCan (bonusCan, new Currency(0,2));

		}
	}

	void HandlePOPOnAcceptButtonClicked (GameObject sender, BonusCannedFood bonusCan)
	{
		AudioController.shared ().PlayAudio ("button");
		BonusPowerUpController.shared().TriggerPowerUp(bonusCan.bonusType,bonusCan.bonusAmount,false); 
		Destroy (sender);
		TimerController.shared ().ResumeTime ();
		AudioController.shared ().SetMainAudioSoundVolume(0.8f);
	}

	void HandlePOPOnFreeButtonClicked (GameObject sender, BonusCannedFood bonusCan)
	{
		AudioController.shared ().PlayAudio ("button");
		//SHOW IKLAN!
		if(bonusCan.categoryType == BonusCategoryType.Positive)
			BonusPowerUpController.shared().TriggerPowerUp(bonusCan.bonusType,bonusCan.bonusAmount,true); 
		Destroy (sender);
		TimerController.shared ().ResumeTime ();
		AudioController.shared ().SetMainAudioSoundVolume(0.8f);
	}

	void HandlePOPOnPayButtonClicked (GameObject sender, BonusCannedFood bonusCan)
	{
		AudioController.shared ().PlayAudio ("pay");
		if(bonusCan.categoryType == BonusCategoryType.Positive)
			BonusPowerUpController.shared().TriggerPowerUp(bonusCan.bonusType,bonusCan.bonusAmount,true); 
		Destroy (sender);
		TimerController.shared ().ResumeTime ();
		AudioController.shared ().SetMainAudioSoundVolume(0.8f);

	}
	

	void HandleOnCanDestroyed (GameObject sender)
	{
		_cannedFoodObject.Remove (sender);
		Destroy (sender);
	}


	bool isChancesHit(float chances)
	{
		int randomResult = Random.Range (1, 100);
		return randomResult <= chances;
	}


	private InstantCannedFood GetInstantCannedFood(BonusCannedFoodType bonusType)
	{
		foreach (InstantCannedFood item in CannedFoodInstantBonus) {
			if(item.bonusType == bonusType)
			{
				return item;
			}
		}
		return null;
		
	}


	private BonusCannedFood GetAnyNegativeOnlyCannedFood()
	{
		return CannedFoodBonus.Where(x=> x.categoryType == BonusCategoryType.Negative).Random();
	}
	private BonusCannedFood GetAnyPositiveOnlyCannedFood()
	{
		return CannedFoodBonus.Where(x=> x.categoryType == BonusCategoryType.Positive).Random();
	}
	
	private BonusCannedFood GetBonusCannedFood(BonusCannedFoodType bonusType)
	{
		foreach (BonusCannedFood item in CannedFoodBonus) {
			if(item.bonusType == bonusType)
			{
				return item;
			}
		}
		return null;
		
	}
	

	private StandardCannedFood GetCannedFoodData(LevelMultiplierType levelMultiplierType)
	{
		foreach (StandardCannedFood item in CannedFoodData) {
			if(item.levelMultiplier == levelMultiplierType)
			{
				return item;
			}
		}
		return null;
	}

}
