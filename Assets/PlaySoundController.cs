using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaySoundController : MonoBehaviour {

	public List<AudioClip> sounds;

	public void PlaySound(int soundIndex)
	{
		Debug.Log("sound played "+ soundIndex);

	}
}
