using UnityEngine;
using System.Collections;

public class Singleton<T> {

	static T instance;

	public static T shared()
	{
		if (instance == null)
			instance = default(T);
		return (T)instance;	
	}

}
