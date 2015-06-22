using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CannedFoodContent : MonoBehaviour {

	public CannedFoodItem can;

	public void SetItem(CannedFoodItem can)
	{
		this.can = can;
		this.GetComponent<Image> ().sprite = can.canSprite;
	
	}

}
