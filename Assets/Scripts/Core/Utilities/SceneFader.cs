using UnityEngine;
using System.Collections;

	[RequireComponent(typeof(GUITexture))]
	public class SceneFader : MonoBehaviour
	{
		public delegate void SceneFaderDelegate ();

		public event SceneFaderDelegate OnFadeOutCompleted;
		public event SceneFaderDelegate OnFadeInCompleted;

		public float fadeSpeed = 5.0f;
		public bool isFadingOut;
		public bool isFadingIn;

		void Awake ()
		{
			guiTexture.texture = new Texture2D (Screen.width, Screen.height);
			guiTexture.pixelInset = new Rect (0, 0, Screen.width, Screen.height);
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;
		}

		void FadeToClear ()
		{
			guiTexture.color = Color.Lerp (guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
		}

		void FadeToBlack ()
		{
			guiTexture.color = Color.Lerp (guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
		}

		void Update ()
		{
			if (isFadingIn) {
				FadeIn ();
			}


			if (isFadingOut) {
				FadeOut ();
			}
		}
		 
		public void FadeIn ()
		{
			guiTexture.enabled = true;
			isFadingIn = true;
			FadeToClear ();
			if (guiTexture.color.a <= .05f) {
			
				guiTexture.color = Color.clear;
				guiTexture.enabled = false;
				isFadingIn = false;
				if (OnFadeInCompleted != null)
					OnFadeInCompleted ();
			}

		}

		public void FadeOut ()
		{
			guiTexture.enabled = true;
			isFadingOut = true;
			FadeToBlack ();

			if (guiTexture.color.a >= 0.95f) {
				guiTexture.color = Color.black;
				isFadingOut = false;
				if (OnFadeOutCompleted != null)
					OnFadeOutCompleted ();
			}


		}
}

