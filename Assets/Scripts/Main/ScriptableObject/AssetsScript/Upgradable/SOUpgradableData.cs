using UnityEngine;
using System.Collections.Generic;

public class SOUpgradableData : ScriptableObject {

	public string upgradeName;
	public string upgradeDetail;
	public string upgradeUnit;
	public UpgradableType UpgradeType;
	public List<UpgradeData> upgradeData;
	
}

[System.Serializable]
public class UpgradeData
{
	public int level;
	public Currency price;
	public float value;
}