using UnityEngine;
using UnityEditor;
using System.Collections;

public class UpgradableCreator : SOAssetCreator {

	[MenuItem("Assets/Create/Upgradeable")]
	public static void createUpgradeable ()
	{
		CreateObject<SOUpgradableData> ("upgrade");
	}

}
