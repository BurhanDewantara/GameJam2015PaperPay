using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialSwipeController : MonoBehaviour {

	public GameObject leftTutorial;
	public GameObject rightTutorial;
	public GameObject topTutorial;
	public GameObject bottomTutorial;

	public List<SOColor> colors;

	public void Awake()
	{
		SetTutorialColor (
			colors.GetRange (0,2),
			colors.GetRange (1,2),
			colors.GetRange (3,2),
			colors.GetRange (4,2)
		);
	}


	public void SetTutorialColor(List<SOColor> left,List<SOColor> right,List<SOColor> top,List<SOColor> bottom)
	{
		leftTutorial.GetComponent<TutorialSwipeInfoController> ().SetColor (left);
		rightTutorial.GetComponent<TutorialSwipeInfoController> ().SetColor (right);
		topTutorial.GetComponent<TutorialSwipeInfoController> ().SetColor (top);
		bottomTutorial.GetComponent<TutorialSwipeInfoController> ().SetColor (bottom);


	}
}
