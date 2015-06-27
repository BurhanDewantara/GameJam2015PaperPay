using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundSwitcher : MonoBehaviour {

	public Sprite normalImage;
	public Sprite reverseImage;

	public void SwitchMode(GamePlayModeType mode)
	{
		Sprite curr = null;
		switch (mode) 
		{
			case GamePlayModeType.Say_The_Color : curr = normalImage; break;
			case GamePlayModeType.Say_The_Word : curr = reverseImage; break;
		}
		this.GetComponent<Image> ().sprite = curr;
	}
}
