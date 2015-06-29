using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Artoncode.Core;

public class CannedFoodController : SingletonMonoBehaviour< CannedFoodController> {

	public List<StandardCannedFood> StandardCannedFood;

	public StandardCannedFood GetCannedFoodItemByMultiplier(LevelMultiplierType multiplier)
	{

		foreach (StandardCannedFood item in StandardCannedFood) {
			if(item.levelMultiplier == multiplier)
				return item;
		}
		return null;

	}

}
