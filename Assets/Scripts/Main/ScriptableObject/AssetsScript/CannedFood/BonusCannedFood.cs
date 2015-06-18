using UnityEngine;
using System.Collections;

public enum BonusCannedFoodType
{
	InstantCoin,
	TimePlus,
	BonusGem,
	TapToSlide,
	CanCan, // double can bonus
	TimeMinus,
	BadCan,
	SwitchPlayMode,
}

public class BonusCannedFood : SOCannedFood {
	public BonusCannedFoodType bonusType; 
}
