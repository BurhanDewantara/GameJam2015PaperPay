using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum SwipeDirectionType
{
	Top,
	Left,
	Right,
	Bottom,
};

public class TutorialSwipeInfoController : MonoBehaviour {

	public SwipeDirectionType direction;

	public GameObject titleText;
	public GameObject swipeIcon;

	public bool isTrashDirection = false;	
	


	private float _range = 50;
	private float _movement =0;

	private float time;
	private float waitTime;
	private float waitTimeLimit = 0.06f;
	private float delayTimeLimit = 0.1f;

	private Vector3 iconOriPos;

	void Awake()
	{
		swipeIcon.GetComponent<TutorialSwipeIconViewer> ().SetDirection (direction);
		iconOriPos = swipeIcon.GetComponent<RectTransform> ().localPosition;
		waitTime = 0;

	}

	public void SetColor(List<SOColor> socolors)
	{
		string txt = "";

		if (isTrashDirection) {
			txt = GameHelper.SetColorInText(Color.grey,"Others");
		} 
		else {
			foreach (SOColor socolor in socolors) {
				if(txt != "")
					txt += ", ";
				txt += socolor.ColorToRichText(socolor.colorType.ToString());
			}
		}

		txt += " Swipe to " + direction.ToString ();
		titleText.GetComponent<Text>().text = txt;
	}



	void FixedUpdate()
	{
		Vector3 calcPos = Vector3.zero;
		switch (direction) {
			case SwipeDirectionType.Bottom 	: calcPos = new Vector3(0,-1,0); break;
			case SwipeDirectionType.Top 	: calcPos = new Vector3(0,1,0); break;
			case SwipeDirectionType.Right 	: calcPos = new Vector3(1,0,0); break;
			case SwipeDirectionType.Left 	: calcPos = new Vector3(-1,0,0); break;
		}

		time += Time.deltaTime;

		if (time >= delayTimeLimit) {
			time = 0;
			if(_movement < _range)
			{
				_movement += Time.deltaTime * 510;
			}
			else if (_movement >= _range)
			{
				waitTime+= Time.deltaTime;
				if(waitTime >= waitTimeLimit)
				{ 
					Debug.Log (waitTime);
					waitTime = 0;
					_movement = -_range/2;
				}
			}
			swipeIcon.GetComponent<RectTransform> ().localPosition = iconOriPos + calcPos * _movement;

		}




	}

}
