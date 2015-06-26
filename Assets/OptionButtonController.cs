using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionButtonController : MonoBehaviour {

	public delegate void OptionButtonControllerDelegate(GameObject sender);
	public event OptionButtonControllerDelegate OnButtonClicked;
	public GameObject mainText;

	void Awake()
	{
		this.GetComponent<Button> ().onClick.AddListener (ButtonClicked);
	}

	void ButtonClicked()
	{
		if (OnButtonClicked != null)
			OnButtonClicked (this.gameObject);
	}


	public void SetText(string txt)
	{
		mainText.GetComponent<Text> ().text = txt;
	}

}
