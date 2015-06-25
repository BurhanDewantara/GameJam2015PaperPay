using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonIconViewer : MonoBehaviour {

	public Sprite freeButton;
	public Sprite payButton;
	public Sprite skipButton;
	public Sprite acceptButton;

	public void SetButton(PowerUpChoicesButtonType type)
	{
		Sprite curr = null;
		
		switch (type) {
		case PowerUpChoicesButtonType.Accept: 
			curr = acceptButton;
			break;
		case PowerUpChoicesButtonType.Free: 
			curr = freeButton;
			break;
		case PowerUpChoicesButtonType.Pay: 
			curr = payButton;
			break;
		case PowerUpChoicesButtonType.Skip: 
			curr = skipButton;
			break;
		}
		
		this.GetComponent<Image> ().sprite = curr;
	}
}
