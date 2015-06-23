using UnityEngine;
using System.Collections;

public class TimerHandler : MonoBehaviour
{
	bool tick;

	void Awake ()
	{
		tick = true;
	}

	public void Ticking ()
	{
		if (tick) {
			this.GetComponent<RectTransform> ().rotation = Quaternion.Euler (new Vector3 (0, 0, 15));
			tick = false;
		} else {
			this.GetComponent<RectTransform> ().rotation = Quaternion.Euler (new Vector3 (0, 0, -15));
			tick = true;
		}

	}
}
