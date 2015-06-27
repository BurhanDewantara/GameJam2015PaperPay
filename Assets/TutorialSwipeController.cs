using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TutorialSwipeController : MonoBehaviour {

	public GameObject titleText;

	public GameObject leftTutorial;
	public GameObject rightTutorial;
	public GameObject topTutorial;
	public GameObject bottomTutorial;

	void Start()
	{
		string text = "";
			switch (PaperGameManager.shared().playMode)
			{
			case GamePlayModeType.Say_The_Color : 
			text = "Slide the paper according to the <color=#00AA00>COLOR</color> not the <color=#AA0000>WORD</color>!";
			break;
			case GamePlayModeType.Say_The_Word : 
			text = "Slide the paper according to the <color=#AA0000>WORD</color> not the <color=#00AA00>COLOR</color>!";
				break;
			}
		titleText.GetComponent<Text> ().text = text;
	}

	public void SetTutorialColor(List<SOColor> left,List<SOColor> top,List<SOColor> right,List<SOColor> bottom)
	{
		leftTutorial.GetComponent<TutorialSwipeInfoController> ().SetColor (left);
		topTutorial.GetComponent<TutorialSwipeInfoController> ().SetColor (top);
		rightTutorial.GetComponent<TutorialSwipeInfoController> ().SetColor (right);
		bottomTutorial.GetComponent<TutorialSwipeInfoController> ().SetColor (bottom);


	}
}
