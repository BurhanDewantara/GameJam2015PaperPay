using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CurrencyViewer : MonoBehaviour {

	public CurrencyType currencyType;
	public bool isPlayerCurrency = false;

	public GameObject icon;
	public GameObject text;

	private Currency _currency;

	public void SetCurrency(Currency currency)
	{
		_currency = currency;
	}

	void Refresh()
	{
		icon.GetComponent<CurrencyIconViewer> ().SetCurrencyType (currencyType);
		switch (currencyType) {
		
		case CurrencyType.Coin : 
			text.GetComponent<Text> ().text = CurrencyManager.shared().PlayerCurrency.coin.ToString();
			break;
		case CurrencyType.Gem : 
			text.GetComponent<Text> ().text = CurrencyManager.shared().PlayerCurrency.gem.ToString();
			break;
		}
	}

	void Update()
	{
		if (isPlayerCurrency) {
			if(_currency != CurrencyManager.shared ().PlayerCurrency)
			{
				_currency = CurrencyManager.shared ().PlayerCurrency;
				Refresh();
			}
		}

	}

}
