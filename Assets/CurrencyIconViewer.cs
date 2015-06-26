using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrencyIconViewer : MonoBehaviour {


	public Sprite GemSprite;
	public Sprite CoinSprite;

	public void SetCurrencyType(CurrencyType type)
	{
		Sprite curr = null;

		switch (type) {
		case CurrencyType.Coin: 
			curr = CoinSprite;
			break;
		case CurrencyType.Gem: 
			curr = GemSprite;
			break;
		}

		this.GetComponent<Image> ().sprite = curr;
	}
}
