using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Artoncode.Core.Data;

public static class GameHelper
{
	public static void ResetData()
	{
		DataManager.defaultManager.reset ();
		DataManager.defaultManager.save ();
		
		CurrencyManager.shared ().Reset ();
		PlayerStatisticManager.shared ().Reset ();
		PlayerUpgradableDataManager.shared ().Reset ();

	}

	public static void GetCoins()
	{
		CurrencyManager.shared().AddMoney(new Currency(9999999,999999));
	}

	public static void Upgrade(UpgradableType key)
	{
		PlayerUpgradableDataManager.shared ().Upgrade (key);
	}
}
