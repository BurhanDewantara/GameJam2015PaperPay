using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameOverController : MonoBehaviour {

	public GameObject gameoverCanPrefab;
	public GameObject canContainer;

	public GameObject cannedOverPrefab;

	public GameObject totalPanel;
	public GameObject nextButton;

	private Dictionary<LevelMultiplierType, int> _collectedCannedFood;
	private List<GameObject> _gameoverItemList;
	private List<LevelMultiplierType> cannedKeys;

	private int _idx =0;

	private Currency _totalPrize = new Currency ();

	public void Awake()
	{

		_gameoverItemList = new List<GameObject> ();
		totalPanel.SetActive(false);
		nextButton.SetActive (false);

		nextButton.GetComponent<Button> ().onClick.AddListener (ButtonNextOnClick);
	}

	private void ButtonNextOnClick()
	{
		LevelController.shared ().LoadLevel ("Upgrade");
	}


	public void SetCannedFood(Dictionary<LevelMultiplierType, int> collectedCannedFood)
	{
		_collectedCannedFood = collectedCannedFood;
		cannedKeys = new List<LevelMultiplierType> (_collectedCannedFood.Keys);
	}

	public void ShowScore()
	{
		CreateGameOverItemList (_idx++);
	}



	public void CreateGameOverItemList(int idx)
	{
		if (idx < cannedKeys.Count) {
			SOCannedFood cannedFood = CannedFoodController.shared ().GetCannedFoodItemByMultiplier (cannedKeys [idx]);
			CannedFoodItem foodItem = new CannedFoodItem (cannedFood);
			int quantity = _collectedCannedFood [cannedKeys [idx]];		

			if(quantity <= 0)
			{
				this.CreateGameOverItemList(this._idx++);
				return;
			}

			Currency prize = new Currency((int)(quantity * foodItem.canMultiplier),0);

			switch (cannedKeys [idx]) {
			
			case LevelMultiplierType.InstantGem : 
				prize = new Currency(0,(int)(quantity * foodItem.canMultiplier));
				break;
			case LevelMultiplierType.Negative1 : 
				Debug.Log(prize);
				Debug.Log(UpgradableDataController.shared().GetPlayerUpgradeDataValue(UpgradableType.ReduceMistakePaperCost));
				prize -= prize * UpgradableDataController.shared().GetPlayerUpgradeDataValue(UpgradableType.ReduceMistakePaperCost) / 100.0f;
				Debug.Log(prize);
				break;
			default:
				prize += prize * UpgradableDataController.shared().GetPlayerUpgradeDataValue(UpgradableType.CanMultiplier);
				break;
			}

			_totalPrize += prize;
			GameObject obj = CreateGameOverCannedFood ();
			obj.GetComponent<RectTransform>().SetParent(canContainer.GetComponent<RectTransform>(),false);
			obj.GetComponent<GameOverCanContentViewer> ().SetCan (foodItem, quantity,prize);
			
			_gameoverItemList.Add(obj);

		} else {
		
			//SHOW TOTAL
			totalPanel.SetActive(true);
			nextButton.SetActive (true);
			totalPanel.GetComponent<CurrencyViewer>().SetCurrency(_totalPrize);
			CurrencyManager.shared().AddMoney(_totalPrize);
			StartCoroutine(CreateCannedOver());
		}

	}


	public IEnumerator CreateCannedOver()
	{
		yield return new WaitForSeconds (0.2f);
		GameObject obj = Instantiate (cannedOverPrefab) as GameObject;
		obj.GetComponent<RectTransform> ().SetParent (this.GetComponent<RectTransform> (),false);
	}

	public GameObject CreateGameOverCannedFood()
	{
		GameObject obj = Instantiate (gameoverCanPrefab) as GameObject;
		obj.GetComponent<GameOverCanContentViewer>().OnAnimationDone += HandleOnAnimationDone;

		return obj;

	}

	void HandleOnAnimationDone (GameObject sender)
	{
		Debug.Log (sender);
		CreateGameOverItemList (_idx++);
	}




}
