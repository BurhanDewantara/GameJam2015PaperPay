using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradableController : MonoBehaviour {

	public List<GameObject> UpgradeList;

	public void Start()
	{
		foreach (GameObject upgradeGameObject in UpgradeList) {
			upgradeGameObject.GetComponent<PowerUpUpgradeController>().OnUpgradeButtonPressed += HandleOnUpgradeButtonPressed;
		}
	}

	void HandleOnUpgradeButtonPressed (GameObject sender)
	{
		foreach (GameObject upgradeGameObject in UpgradeList) {
			upgradeGameObject.GetComponent<PowerUpUpgradeController> ().Refresh();
		}
	}

}
