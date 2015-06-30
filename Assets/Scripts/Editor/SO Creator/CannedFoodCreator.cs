using UnityEngine;
using UnityEditor;
using System.Collections;

public class CannedFoodCreator : SOAssetCreator {

	[MenuItem("Assets/Create/StandardCannedFood")]
	public static void createStandard ()
	{
		CreateObject<StandardCannedFood> ("SCF");
	}
	[MenuItem("Assets/Create/BonusCannedFood")]
	public static void createBonus ()
	{
		CreateObject<BonusCannedFood> ("BCF");
	}
	[MenuItem("Assets/Create/InstantCannedFood")]
	public static void createGem ()
	{
		CreateObject<InstantCannedFood> ("ICF");
	}

}
