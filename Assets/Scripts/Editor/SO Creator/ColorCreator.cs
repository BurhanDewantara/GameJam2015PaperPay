using UnityEngine;
using UnityEditor;
using System.Collections;

public class PaperCreator : SOAssetCreator {

	[MenuItem("Assets/Create/Color")]
	public static void createPaper ()
	{
		CreateObject<SOColor> ("Color");
	}

}
