using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpPopUpController : MonoBehaviour {


	public delegate void PowerUpPopUpControllerDelegate(GameObject sender, BonusCannedFood bonusCan);
	public event PowerUpPopUpControllerDelegate OnFreeButtonClicked;
	public event PowerUpPopUpControllerDelegate OnAcceptButtonClicked;
	public event PowerUpPopUpControllerDelegate OnPayButtonClicked;



	public BonusCannedFood bonusCan;

	public GameObject bonusImage;
	public GameObject bonusText;

	public GameObject bonusFreeButton;
	public GameObject bonusPayButton;
	public GameObject bonusAcceptButton;

	private Currency _price;


	void Awake()
	{
		bonusFreeButton.GetComponent<Button> ().onClick.AddListener (FreeButtonClick);
		bonusPayButton.GetComponent<Button> ().onClick.AddListener (PayButtonClick);
		bonusAcceptButton.GetComponent<Button> ().onClick.AddListener (AcceptButtonClick);
	}

	void FreeButtonClick()
	{
		if (OnFreeButtonClicked != null)
			OnFreeButtonClicked (this.gameObject,bonusCan);
	}
	void PayButtonClick()
	{
		CurrencyManager.shared ().PayMoney (_price);
		if (OnPayButtonClicked != null)
			OnPayButtonClicked (this.gameObject,bonusCan);

	}
	void AcceptButtonClick()
	{
		if (OnAcceptButtonClicked != null)
			OnAcceptButtonClicked (this.gameObject,bonusCan);
	}

	void Start()
	{
		SetBonusCan(bonusCan,new Currency(10,0));
	}

	public void SetBonusCan(BonusCannedFood bonusCan,Currency price)
	{
		this.bonusCan = bonusCan;
		this._price = price;
		bonusImage.GetComponent<Image> ().sprite = bonusCan.bonusImage;


		string buttonText = "";
		switch (bonusCan.categoryType) {
		case BonusCategoryType.Negative:
			buttonText = "Skip this";
			break;
		case BonusCategoryType.Positive:
			buttonText = "Double this";
			break;
		}

		bonusText.GetComponent<Text> ().text = bonusCan.bonusText;


		bonusFreeButton.GetComponent<OptionButtonController> ().SetText (buttonText);
		bonusPayButton.GetComponent<PriceOptionButtonController> ().SetText (buttonText);
		bonusPayButton.GetComponent<PriceOptionButtonController> ().SetPrice (price);

		bonusPayButton.GetComponent<Button> ().interactable = CurrencyManager.shared ().IsAffordable (price);

	}




}
