using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaySoundController : MonoBehaviour {

	public List<AudioClip> sounds;
	private AudioSource aSource;

	public void Awake()
	{
		aSource = this.GetComponent<AudioSource> ();
	}
	public void PlaySound(int soundIndex)
	{
//		AudioSource.PlayClipAtPoint (sounds [soundIndex], Vector3.zero);
		aSource.PlayOneShot (sounds [soundIndex]);
//		aSource.clip = sounds [soundIndex];
//		aSource.Play ();
//		Debug.Log("sound played "+ soundIndex);

	}
}
