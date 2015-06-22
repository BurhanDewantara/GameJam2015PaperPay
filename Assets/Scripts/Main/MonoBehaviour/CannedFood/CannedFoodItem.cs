using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class CannedFoodItem {

	public float canMultiplier;
	public Sprite canSprite;

	public CannedFoodItem (StandardCannedFood can)
	{
		canMultiplier = can.multiplier;
		canSprite = can.canSprite;
	}
}

