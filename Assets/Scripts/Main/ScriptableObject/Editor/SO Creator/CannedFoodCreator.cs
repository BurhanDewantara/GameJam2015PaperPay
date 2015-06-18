using UnityEngine;
using UnityEditor;
using System.Collections;

public class CannedFoodCreator : SOAssetCreator {

	[MenuItem("Assets/Create/CannedFood")]
	public static void createStandard ()
	{
		CreateObject<StandardCannedFood> ("CannedFood");
	}
	[MenuItem("Assets/Create/BonusCannedFood")]
	public static void createBonus ()
	{
		CreateObject<BonusCannedFood> ("BonusCannedFood");
	}

}
