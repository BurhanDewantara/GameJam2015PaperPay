using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Artoncode.Core;

public class UpgradableDataController : SingletonMonoBehaviour<UpgradableDataController> {

	public List<SOUpgradableData> upgradeList;

	public float GetDataValue(UpgradableType key)
	{
		return GetUpgradeData(key).value;
	}

	public Currency GetDataPrice(UpgradableType key)
	{
		return GetUpgradeData(key).price;
	}

	private UpgradeData GetUpgradeData(UpgradableType key)
	{
		SOUpgradableData data = GetData(key);
		int level = PlayerUpgradableDataManager.shared ().GetUpgradeDataLevel (key);

		return data.upgradeData[level];
	}



	private SOUpgradableData GetData(UpgradableType key)
	{
		foreach (SOUpgradableData item in upgradeList) {
			if(item.UpgradeType == key)
				return item;
		}
		return null;	

	}
}
