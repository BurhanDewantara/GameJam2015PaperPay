using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Artoncode.Core;

public class CannedFoodMachineController : SingletonMonoBehaviour<CannedFoodMachineController> {

	public GameObject canContainer;
	public GameObject canPrefab;

	public List<StandardCannedFood> CannedFoodData;
	public List<BonusCannedFood> CannedFoodBonus;

	private List<GameObject> _cannedFoodObject;

	void Start()
	{
		_cannedFoodObject = new List<GameObject> ();

	}

	void MoveAllCan()
	{
		foreach (GameObject can in _cannedFoodObject) {
			can.GetComponent<CannedFoodContentViewer>().Slide();
		}
	}

	public void Create()
	{ 
		MoveAllCan ();

		GameObject obj = CreateCan (canPrefab);
		obj.GetComponent<RectTransform> ().SetParent (canContainer.GetComponent<RectTransform> (),false);
		_cannedFoodObject.Add (obj);
	}

	GameObject CreateCan(GameObject canPrefab)
	{
		GameObject obj = Instantiate (canPrefab) as GameObject;
		obj.GetComponent<CannedFoodContentViewer>().OnCanDestroyed += HandleOnCanDestroyed;
		obj.GetComponent<CannedFoodContentViewer>().OnCanClicked += HandleOnCanClicked; ;
		return obj;
	}

	void HandleOnCanClicked (GameObject sender)
	{
		// DO SOMETHING!
		Destroy (sender);
	}

	void HandleOnCanDestroyed (GameObject sender)
	{
		_cannedFoodObject.Remove (sender);
		Destroy (sender);
	}

}
