using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class CannedFoodItem {

	public float canMultiplier;
	public Sprite canSprite;
	public SOCannedFood canFood;

	public bool isPowerUp
	{
		get{
			return (!(canFood is StandardCannedFood));
		}

	}
	public CannedFoodItem (SOCannedFood can)
	{
		this.canFood = can;

		if (!isPowerUp) {
			canMultiplier = (can as StandardCannedFood).multiplier;
		}
		canSprite = can.canSprite;
	}
}

