using UnityEngine;
using UnityEditor;
using System.Collections;

public class FishNameCreator : SOAssetCreator {

	[MenuItem("Assets/Create/FishNameList")]
	public static void createPaper ()
	{
		CreateObject<SOFish> ("FishDatabase");
	}

}
