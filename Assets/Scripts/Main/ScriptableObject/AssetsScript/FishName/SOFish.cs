using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SOFish : ScriptableObject{

	public List<string> names;
	public List<Sprite> images;

	public string GetRandomName()
	{
		return names[Random.Range (0, names.Count)]; 
	}

	public Sprite GetRandomImage()
	{
		return images[Random.Range (0, images.Count)]; 
	}

}
