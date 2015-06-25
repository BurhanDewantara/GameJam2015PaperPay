using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Artoncode.Core;

public class CannedFoodMachineController : SingletonMonoBehaviour<CannedFoodMachineController> {

	public GameObject canContainer;
	public GameObject canPrefab;

	public List<StandardCannedFood> CannedFoodData;
	public List<BonusCannedFood> CannedFoodBonus;
	public List<InstantCannedFood> CannedFoodInstantBonus;

	private List<GameObject> _cannedFoodObject;

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

	public void CreateCan(LevelMultiplierType levelMultiplierType,float gemChances     = 10,float bonusChances = 10)
	{ 
		MoveAllCan ();
		GameObject obj = CreateCanObject (canPrefab);
		obj.GetComponent<RectTransform> ().SetParent (canContainer.GetComponent<RectTransform> (),false);
		obj.GetComponent<RectTransform> ().SetAsFirstSibling ();

		SOCannedFood CanFood = GetCannedFoodData (levelMultiplierType);

		if (isBonusCannedFoodIsEligibleToProduce () && levelMultiplierType >= LevelMultiplierType.Positive1) {
			if (isChancesHit (gemChances)) 
			{
				CanFood = GetInstantCannedFood(BonusCannedFoodType.BonusGem);
			} 
			else if (isChancesHit (bonusChances)) 
			{ 
				int randresult = Random.Range(0,7);
				CanFood = GetBonusCannedFood((BonusCannedFoodType)randresult);
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
			{
				break;
			}
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
			BonusCannedFoodType bonustype = (can as BonusCannedFood).bonusType;

			//SHOW POP UP TO ACCEPT;

			//FASTEST WAY
			BonusPowerUpController.shared().TriggerPowerUp(bonustype); 

		} 
		else if (can is InstantCannedFood)
		{

			BonusCannedFoodType bonustype = (can as InstantCannedFood).bonusType;
			switch (bonustype) 
			{
				case BonusCannedFoodType.BonusGem : BonusPowerUpController.shared().TriggerPowerUp(BonusCannedFoodType.BonusGem); break;
			}
		}

		_cannedFoodObject.Remove (sender);
		Destroy (sender);
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
