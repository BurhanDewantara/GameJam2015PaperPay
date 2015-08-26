using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class PopUpAndFade : MonoBehaviour {

	float fadingTime = 2;
	float timer;
	float fadeDelta;

	bool isActive = false;
	void Start()
	{
		isActive = true;
		this.GetComponent<CanvasGroup> ().interactable = false;
		this.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}


	void Update()
	{
		if (!isActive)
			return;

		timer += Time.deltaTime;
	
			this.GetComponent<CanvasGroup> ().alpha = 1 - (timer / fadingTime);
			this.GetComponent<RectTransform> ().localPosition += new Vector3 (0, Time.deltaTime * 100, 0);
			this.GetComponent<RectTransform> ().localScale += new Vector3 ( Time.deltaTime , Time.deltaTime ,0);
			
			
			if (timer >= fadingTime) {
				isActive = false;
				Destroy();

		}


	}

	void Destroy()
	{
		Destroy (this.gameObject);
	}
}
