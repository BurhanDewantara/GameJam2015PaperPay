using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Artoncode.Core;

[System.Serializable]
public class AudioItem
{
	public string key;
	public AudioClip audio;
}

public class AudioController : SingletonMonoBehaviour<AudioController> {

	public List<AudioItem> audios;

	public void PlayAudio(string key)
	{
		foreach (AudioItem audio in audios) {
			if(audio.key == key)
			{
				AudioSource.PlayClipAtPoint(audio.audio,Vector3.zero);
				break;
			}
		}
	}

	public void SetMainAudioSoundVolume(float vol)
	{
		this.GetComponent<AudioSource> ().volume = vol;

	}
}
