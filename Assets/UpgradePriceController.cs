using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UpgradePriceController : MonoBehaviour {

	public GameObject priceIcon;
	public GameObject priceText;
	public GameObject priceMaxText;

	public Currency price;

	public void SetPrice(Currency price)
	{
		priceMaxText.SetActive (false);
		this.price = price;
		KeyValuePair<CurrencyType,int> kvp = price.SplitCurrency ();

		priceIcon.GetComponent<CurrencyIconViewer> ().SetCurrencyType (kvp.Key);
		priceText.GetComponent<Text> ().text = kvp.Value.ToString();
	}


	public void SetIsMax()
	{
		priceIcon.SetActive (false);
		priceText.SetActive (false);
		priceMaxText.SetActive (true);
		priceMaxText.GetComponent<Text> ().text = "MAX";

	}

}
