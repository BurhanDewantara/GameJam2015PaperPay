using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverCanContentViewer : MonoBehaviour {


	public delegate void GameOverCanContentViewerDelegate (GameObject sender);
	public event GameOverCanContentViewerDelegate OnAnimationDone;

	public CannedFoodItem canItem;
	public GameObject canImage;
	public GameObject canQtyText;
	public GameObject totalPriceText;

	private float _speed = 0.3f;

	public void Awake()
	{
		canQtyText.SetActive (false);
		canImage.SetActive (false);
		totalPriceText.SetActive (false);
	}

	public void SetCan(CannedFoodItem item,int quantity,Currency prize)
	{
		canItem = item;

		StartCoroutine(SetCanImage());
		StartCoroutine(SetQuantity(quantity));
		StartCoroutine(SetPrize(prize));

	}

	private IEnumerator SetCanImage()
	{
		yield return new WaitForSeconds(_speed * 1);
		canImage.SetActive (true);
		canImage.GetComponent<Image> ().sprite = canItem.canSprite;
	}

	private IEnumerator SetQuantity(int quantity)
	{
		yield return new WaitForSeconds (_speed * 2);
		canQtyText.SetActive (true);
		canQtyText.GetComponent<Text> ().text = quantity.ToString();
	}

	private IEnumerator SetPrize(Currency prize)
	{
		yield return new WaitForSeconds (_speed * 3);
		totalPriceText.SetActive (true);
		totalPriceText.GetComponent<CurrencyViewer> ().currencyType = prize.SplitCurrency().Key;
		totalPriceText.GetComponent<CurrencyViewer>().SetCurrency(prize);
	
		if (OnAnimationDone != null)
			OnAnimationDone (this.gameObject);
	}



}
