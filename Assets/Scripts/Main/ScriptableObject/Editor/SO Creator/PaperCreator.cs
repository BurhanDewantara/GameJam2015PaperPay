using UnityEngine;
using UnityEditor;
using System.Collections;

public class PaperCreator : SOAssetCreator {

	[MenuItem("Assets/Create/Paper/CompanyPaper")]
	public static void createCompanyPaper ()
	{
		CreateObject<CompanyPaper> ("coPaper");
	}

	[MenuItem("Assets/Create/Paper/AdsPaper")]
	public static void createAdsPaper ()
	{
		CreateObject<AdsPaper> ("adsPaper");
	}
}
