using UnityEngine;
using System.Collections;

using Artoncode.Core;

public class TapToProgress : MonoBehaviour,IInputManagerDelegate {

	public int tapToProgress = 2;
	public int tapCount = 0;

	public GameObject textGameObject;
	public string nextScene;

	public void touchStateChanged (TouchInput []touches)
	{

		TouchInput input = touches [0];
		Debug.Log (input);
		Debug.Log (input.phase);
		switch (input.phase) {
		case TouchPhase.Began : 
			Tapped();
			break;
		}
	}


	public void Tapped()
	{
		tapCount++;
		if (tapCount < tapToProgress) {
			textGameObject.SetActive (true);
		} else {
			ChangeScene();
		}
	}

	public void ChangeScene()
	{
		LevelController.shared().LoadLevel(nextScene);
	}
}
