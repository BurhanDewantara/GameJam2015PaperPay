using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverCanContentViewer : MonoBehaviour {

	public CannedFoodItem canItem;
	public GameObject canImage;
	public GameObject canQtyText;
	public GameObject totalPriceText;

	public void SetCan(CannedFoodItem item,int quantity)
	{
		canItem = item;

		canImage.GetComponent<Image> ().sprite = canItem.canSprite;
		canQtyText.GetComponent<Text> ().text = quantity.ToString();

		Currency price = new Currency ((int)(quantity * canItem.canMultiplier), 0);
		totalPriceText.GetComponent<CurrencyViewer> ().currencyType = CurrencyType.Coin;
		totalPriceText.GetComponent<CurrencyViewer> ().currencyType = CurrencyType.Coin;
		totalPriceText.GetComponent<CurrencyViewer>().SetCurrency(price);
	}




}
