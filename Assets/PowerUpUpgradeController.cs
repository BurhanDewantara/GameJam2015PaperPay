using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpUpgradeController : MonoBehaviour {


	public delegate void PowerUpUpgradeControllerDelegate(GameObject sender);
	public event PowerUpUpgradeControllerDelegate OnUpgradeButtonPressed;

	public SOUpgradableData upgradeData;

	public GameObject levelText;
	public GameObject titleText;
	public GameObject detailText;
	public GameObject upgradeButton;

	private Currency _price;
	private Currency _playerMoney;

	public void Awake()
	{
		_playerMoney = CurrencyManager.shared ().PlayerCurrency;
		upgradeButton.GetComponent<Button> ().onClick.AddListener (Upgrade);
	}

	public void Update()
	{
		if(_playerMoney != CurrencyManager.shared ().PlayerCurrency)
		{
			_playerMoney = CurrencyManager.shared ().PlayerCurrency;
			Refresh();
		}
	}

	public void Upgrade()
	{
		CurrencyManager.shared ().PayMoney (_price);
		PlayerUpgradableDataManager.shared ().Upgrade (upgradeData.UpgradeType);
		if (OnUpgradeButtonPressed != null)
			OnUpgradeButtonPressed (this.gameObject);

	}

	public void Start()
	{
		Refresh ();
	}

	public void Refresh()
	{
		int currentLevel = PlayerUpgradableDataManager.shared ().GetUpgradeDataLevel (upgradeData.UpgradeType);

		levelText.GetComponent<Text> ().text = currentLevel.ToString();
		titleText.GetComponent<Text> ().text = upgradeData.upgradeName;
		detailText.GetComponent<Text> ().text = upgradeData.upgradeDetail;


		if (currentLevel == upgradeData.upgradeData.Count - 1) {

			detailText.GetComponent<Text> ().text += " " + GameHelper.SetColorInText(Color.green,upgradeData.upgradeData [currentLevel].value.ToString()) + upgradeData.upgradeUnit;
			upgradeButton.GetComponent<UpgradePriceController> ().SetIsMax();
			upgradeButton.GetComponent<Button> ().interactable = false;			

		} else {

			detailText.GetComponent<Text> ().text += " from " + GameHelper.SetColorInText(Color.red,upgradeData.upgradeData [currentLevel].value.ToString()) + upgradeData.upgradeUnit;
			detailText.GetComponent<Text> ().text += " -> " + GameHelper.SetColorInText(Color.green,upgradeData.upgradeData [currentLevel+1].value.ToString()) + upgradeData.upgradeUnit;
		
			float discValue = UpgradableDataController.shared().GetPlayerUpgradeDataValue(UpgradableType.ReduceUpgradeCost);

			_price = upgradeData.upgradeData [currentLevel + 1].price - (upgradeData.upgradeData [currentLevel + 1].price * discValue / 100.0f );

			upgradeButton.GetComponent<UpgradePriceController> ().SetPrice (_price);

			upgradeButton.GetComponent<Button> ().interactable = CurrencyManager.shared().IsAffordable(_price);	

		}


	}



}
