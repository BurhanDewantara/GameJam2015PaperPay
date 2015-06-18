using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CannedFoodContent : MonoBehaviour {

	public GameObject sprite;
	public CannedFoodItem can;

	public void SetItem(CannedFoodItem can)
	{
		this.can = can;
		sprite.GetComponent<Image> ().sprite = can.canSprite;
	
	}

}
