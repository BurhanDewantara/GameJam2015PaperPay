using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PriceOptionButtonController : OptionButtonController {

	public CurrencyViewer currencyViewer ;
	public Currency price;

	public void SetPrice(Currency price)
	{
		this.price = price;
		Debug.Log (price);
		currencyViewer.GetComponent<CurrencyViewer> ().isPlayerCurrency = false;
		currencyViewer.GetComponent<CurrencyViewer> ().currencyType = price.SplitCurrency ().Key;
		currencyViewer.GetComponent<CurrencyViewer> ().SetCurrency (price);

	}

}
