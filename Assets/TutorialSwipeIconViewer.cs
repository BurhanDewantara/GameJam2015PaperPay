using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialSwipeIconViewer : MonoBehaviour {

	public Sprite leftSprite;
	public Sprite rightSprite;
	public Sprite topSprite;
	public Sprite bottomSprite;

	
	public void SetDirection(SwipeDirectionType type)
	{
		Sprite curr = null;
		
		switch (type) {
		case SwipeDirectionType.Bottom: 
			curr = bottomSprite;
			break;
		case SwipeDirectionType.Left: 
			curr = leftSprite;
			break;
		case SwipeDirectionType.Right: 
			curr = rightSprite;
			break;
		case SwipeDirectionType.Top: 
			curr = topSprite;
			break;
		}
		
		this.GetComponent<Image> ().sprite = curr;
	}
}