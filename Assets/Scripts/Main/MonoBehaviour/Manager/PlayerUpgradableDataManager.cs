using UnityEngine;
using System.Collections;
using Artoncode.Core;

public class PlayerUpgradableDataManager : Singleton<PlayerUpgradableDataManager> {

	//it only store LEVEL!
	public Hashtable playerUpgradableData;

	public PlayerUpgradableDataManager ()
	{
		Reset ();
	}

	public void Reset ()
	{
		Load ();
	}
	public void Save()
	{
		GameManager.shared ().GameUpgradableData = playerUpgradableData;
	}

 	public void Load()
	{
		playerUpgradableData = GameManager.shared ().GameUpgradableData;
		if (playerUpgradableData == null) {
			playerUpgradableData = new Hashtable();

			playerUpgradableData.Add(UpgradableType.CanMultiplier,0);
			playerUpgradableData.Add(UpgradableType.ChancesBonusCan,0);
			playerUpgradableData.Add(UpgradableType.ChancesBonusGem,0);
			playerUpgradableData.Add(UpgradableType.ExtraTotalBonus,0);
			playerUpgradableData.Add(UpgradableType.PaperSlideSpeed,0);
			playerUpgradableData.Add(UpgradableType.PermanentTime,0);
			playerUpgradableData.Add(UpgradableType.ReduceMistakePaperCost,0);
			playerUpgradableData.Add(UpgradableType.ReduceUpgradeCost,0);

		}
	}



	public string ToString()
	{
		string str = "";
		return str;
	}
}
